using System.ComponentModel;
using System.Numerics;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.ServiceLibrary.Managers;

using EveNatTools.ServiceLibrary.Utilities;

using EVEStandard;
using EVEStandard.Models;
using EVEStandard.Models.API;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class ProductionManager : IProductionManager
    {
        private readonly IMarketManager _marketManager;
        private readonly IBlueprintManager _blueprintManager;
        private readonly ITaxManager _taxManager;

        private readonly EveMailHelperContext _dbContext;

        private readonly BlueprintDbAccess _blueprintDbAccess;
        private readonly IndustryActivityDbAccess _industryActivityDbAccess;
        private readonly EveTypeDbAccess _eveTypeDbAccess;    

        private readonly NormalizedProductionCostDbAccess _normalizedProdcutionCostDbAccess;

        private int MaxAgeInMinutes = 60;

        /// <summary>
        /// fixed at 4%, but not sure Eve change notes says 0.25%
        /// </summary>
        private readonly double SCCsurcharge = 4;
        /// <summary>
        /// Only applicable to alpha clones, set to 0.25%
        /// </summary>
        private readonly double AlphaCloneTax = 0.25;

        public ProductionManager(
            IMarketManager marketManager,
            IBlueprintManager blueprintManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory
            )
        {
            _marketManager = marketManager;
            _blueprintManager = blueprintManager;
            _dbContext = dbContextFactory.CreateDbContext();
            _normalizedProdcutionCostDbAccess = new NormalizedProductionCostDbAccess(_dbContext);
            _blueprintDbAccess = new BlueprintDbAccess(_dbContext);
            _industryActivityDbAccess = new IndustryActivityDbAccess(_dbContext);
            _eveTypeDbAccess = new EveTypeDbAccess(_dbContext);
        }

        public async Task<ProductionPlan> GetProductionPlan(
            IndustryBlueprint blueprint, IList<int> activityFilerIds,
            int regionId, double systemCostIndex, double structureBonuses, double facilityTax,
            double materialModifier,
            bool isAlphaClone
            )
        {
            _ = blueprint ?? throw new ArgumentNullException(nameof(blueprint));
            if (blueprint.TypeId == 0)
                throw new Exception("blueprint.TypeId is zero");

            // Todo: fix static activityfilter id
            var plan = await _blueprintManager.GetBlueprintComponentsList(blueprint, 11);
            if (plan.Product != null && plan.Product.EveId > 0)
            {
                var sellbuyPrice = await _marketManager.ArchivedBuySellPrice(
                    regionId, plan.Product.EveId, MaxAgeInMinutes);
                plan.ProductPricePerUnit = sellbuyPrice;
                foreach (var item in plan)
                {
                    sellbuyPrice = await _marketManager.ArchivedBuySellPrice(
                        regionId, item.EveType.EveId, MaxAgeInMinutes);
                    item.PricePerUnit = sellbuyPrice;
                }
                await AddProductionCosts(plan, systemCostIndex, structureBonuses, facilityTax,
                    materialModifier,  isAlphaClone);
            }
            return plan;
        }

        // handover
        // Buildplan with costs, system ??, station ??, time to run
        // add additional costs to buildplan

        public async Task AddProductionCosts(
            ProductionPlan productionPlan, 
            double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier,
            bool isAlphaClone)
        {
            productionPlan.JobCost = await JobCost(productionPlan.Root, systemCostIndex, structureBonuses,
                facilityTax, isAlphaClone);
            foreach (var component in productionPlan.Root.SubComponents)
            {
                await Produce(component, systemCostIndex, structureBonuses, facilityTax, materialModifier,
                    isAlphaClone);
            }
        }

        protected async Task Produce(
            BlueprintComponent component, 
            double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier,
            bool isAlphaClone)
        {
            // only if subcomponents are present, we continue
            if (!component.SubComponents.Any())
                return;
            // buy or produce?
            BlueprintAnalyzer analyzer = new(component, materialModifier);
            if (analyzer.IsBuyingComponentBetter())
                return;
            // get job costs and store them in the component tree
            // not a good use ...
            component.JobCost = await JobCost(component, systemCostIndex, structureBonuses, facilityTax, isAlphaClone);

            foreach (var subcomponent in component.SubComponents)
            {
                await Produce(subcomponent, systemCostIndex, structureBonuses, facilityTax, materialModifier,
                    isAlphaClone);
            }
        }

        /// <summary>
        /// calculates the Cost of an industry job
        /// </summary>
        /// <remarks>
        /// This is quite complex.
        /// 
        /// Total job cost= Estimated item value × 
        ///                 ((System cost index×Structure bonuses)+Facility tax+SCC surcharge+Alpha clone tax)
        ///                 
        /// Estimated item value = Sum over all Materials of: (Material quantity * Material adjusted price)
        /// 
        /// Material adjusted price can  be found in ESI /markets/prices/
        /// </remarks>
        protected async Task<double> JobCost(
            BlueprintComponent component, double systemCostIndex, double structureBonuses, double facilityTax,
            bool isAlphaClone)
        {
            double totalJobCost = 0;
            double estimatedItemValue = 0;
            foreach (var subcomponent in component.SubComponents)
            {
                var marketPrice =
                    await _marketManager.ArchivedMarketPrice(subcomponent.EveType.EveId, MaxAgeInMinutes);
                estimatedItemValue += subcomponent.Quantity * marketPrice.AdjustedPrice;
            }

            totalJobCost = estimatedItemValue / 100 *
                ((systemCostIndex * structureBonuses) + facilityTax + SCCsurcharge +
                  AlphaCloneTaxIfApplicable(isAlphaClone));
            return totalJobCost;
        }

        protected double AlphaCloneTaxIfApplicable(bool isApplicable)
        {
            if (isApplicable)
                return AlphaCloneTax;
            else
                return 0.0;
        }

        public BuyList DeriveBestPriceBuyListFromPlan(
            ProductionPlan plan, int NumberOfRuns, double materialModifier)
        {
            ProductionPlanAnalyzer analyzer = new(plan, materialModifier);
            BuyList buyList = new()
            {
                Name = plan.ProductName,
                NumberOfRuns = NumberOfRuns,
            };

            // check if it's cost effective to build
            if (analyzer.IsProducingBetter())
            {
                int minimumNumberOfRuns;
                if (!analyzer.NumberOfRunsIsValid(NumberOfRuns, onlyUseBestPricePath:true,
                    out minimumNumberOfRuns))
                    throw new Exception($"Number of Runs must be a multiple of {minimumNumberOfRuns}");
                foreach (var component in plan.Root.SubComponents)
                {
                    buyList.Merge(RecursiveBestPriceBuyList(component, NumberOfRuns, materialModifier));
                }
            }

            return buyList;
        }

        protected BuyList RecursiveBestPriceBuyList(
            BlueprintComponent component, int NumberOfRuns, double materialModifier)
        {
            BlueprintAnalyzer analyzer = new(component, materialModifier);
            BuyList buyList = new();
            if (analyzer.IsBuyingComponentBetter())
            {
                buyList.ItemList.Add(new()
                {
                    // NumberOfRuns is multiplied by all multipliers on the parent's path
                    EveType = component.EveType,
                    Quantity = analyzer.ModifiedQuantity() * NumberOfRuns,
                    Price = analyzer.PriceSum() * NumberOfRuns,
                    Volume = analyzer.VolumeSum() * NumberOfRuns,
                }
                );
                return buyList;
            }

            int subComponentsNumberOfRuns = (int)(NumberOfRuns / component.ForcedQuantityMultiplier);
            foreach (var subComponent in component.SubComponents)
            {
                buyList.Merge(RecursiveBestPriceBuyList(subComponent,
                                                        subComponentsNumberOfRuns,
                                                        materialModifier));
            }
            return buyList;
        }

        public class ProductionCost
        {
            public double JobCost;
            public double ComponentCost;
        }

        public async Task CacheProductionCostAsync(
            ProductionPlan plan, int NumberOfRuns, double materialModifier, int MaxAgeInMinutes)
        {
            double age = await _normalizedProdcutionCostDbAccess.GetAgeForId(plan.Blueprint.Type.EveId);

            if (age == 0 || age > MaxAgeInMinutes)
            {
                var result = DeriveProductionCost(plan, NumberOfRuns, materialModifier);

                await _normalizedProdcutionCostDbAccess.AddOrUpdateAsync(result);
            }

        }

        // Direct build using the top level blueprint and it's required components
        // Bestprice build - replacing buying components by building them if this is cheaper
        // compare to quantity * current marketprice
        public NormalizedProductionCost DeriveProductionCost(
            ProductionPlan plan, int NumberOfRuns, double materialModifier
            //,int AccountSkillLevel, double BrokerRelationsLevel, double FactionStanding, double CorpStanding
            )
        {
            _ = plan.Product ?? throw new Exception("ProductionPlan with empty product");

            ProductionPlanAnalyzer analyzer = new(plan, materialModifier);

            NormalizedProductionCost result = new()
            {
                // MUST BE SET!!!
                EveType = plan.Activity.Type, // connection to activity
                IndustryActivity = plan.Activity,
                NumberOfRuns = NumberOfRuns,
                Product = plan.Product,
                ProductQuantity = plan.ProductQuantity,
                ProductSellPricePerUnit = plan.ProductPricePerUnit.SellPrice,
                ProductBuyPricePerUnit = plan.ProductPricePerUnit.BuyPrice,
                DirectJobCost = plan.JobCost * NumberOfRuns,
            };

            foreach (var subComponent in plan.Root.SubComponents)
            {
                BlueprintAnalyzer blueprintAnalyzer = new(subComponent, materialModifier);
                result.DirectComponentCost += blueprintAnalyzer.PriceSum() * NumberOfRuns;
            }
            // here comes the complex part, selecting the best price articles
            // we are aggregating information (throwing away details about how the price was calculated)
            result.BestPriceJobCost = result.DirectJobCost;
            //result.BestPriceComponentCost = result.BestPriceComponentCost;
            if (analyzer.IsProducingBetter())
            {
                int minimumNumberOfRuns;
                if (!analyzer.NumberOfRunsIsValid(NumberOfRuns, onlyUseBestPricePath: true, 
                    out minimumNumberOfRuns))
                    throw new Exception($"Number of Runs must be a multiple of {minimumNumberOfRuns}");
                result.BestPriceJobCost = plan.JobCost * NumberOfRuns;
                //result.BestPriceComponentCost = 0;
                foreach (var component in plan.Root.SubComponents)
                {
                    var subresult = 
                        RecursiveBestPriceProductionCost(component, NumberOfRuns, materialModifier);
                    result.BestPriceJobCost += subresult.JobCost;
                    result.BestPriceComponentCost += subresult.ComponentCost;
                }
            }

            // now artificially set the sums (normally loaded from db)
            result.DirectCostSum = result.DirectJobCost + result.DirectComponentCost;
            result.BestPriceSum = result.BestPriceJobCost + result.BestPriceComponentCost;
            result.ProductCostSum = result.ProductQuantity * result.ProductSellPricePerUnit * NumberOfRuns;

            //return AddSellTax(result, AccountSkillLevel, BrokerRelationsLevel, FactionStanding, CorpStanding);
            return result;
        }

        protected ProductionCostExtended AddSellTax(NormalizedProductionCost cost,
            int AccountSkillLevel, double BrokerRelationsLevel, double FactionStanding, double CorpStanding)
        {
            //
            ProductionCostExtended costExt = new();
            costExt.CopyShallow(cost);
            costExt.Profit.MarketValueTaxes = _taxManager.CalculateSellOrderTaxes(costExt.Profit.MarketValue,
                AccountSkillLevel, BrokerRelationsLevel, FactionStanding, CorpStanding);
            costExt.Profit.ImmediateSellTaxes = _taxManager.CalculateImmediateSellTaxes(costExt.Profit.MarketValue,
                BrokerRelationsLevel, FactionStanding, CorpStanding);
            return costExt;
        }

        protected ProductionCost RecursiveBestPriceProductionCost(
            BlueprintComponent component, int NumberOfRuns, double materialModifier)
        {
            BlueprintAnalyzer blueprintAnalyzer = new(component, materialModifier);
            var result = new ProductionCost();
            BuyList buyList = new();
            if (blueprintAnalyzer.IsBuyingComponentBetter())
            {
                result.ComponentCost = blueprintAnalyzer.PriceSum() * NumberOfRuns;
                return result;
            }
                        
            int subComponentsNumberOfRuns = (int)(NumberOfRuns / component.ForcedQuantityMultiplier);
            result.JobCost += component.JobCost * subComponentsNumberOfRuns;
            foreach (var subComponent in component.SubComponents)
            {
                var subresult = RecursiveBestPriceProductionCost(subComponent, subComponentsNumberOfRuns, materialModifier);
                result.JobCost += subresult.JobCost;
                result.ComponentCost += subresult.ComponentCost;
            }
            return result;
        }

        public async Task PreprocessBlueprintsForActivity(
            IProgress<Tuple<int,int>> progressAndMax,
            int activityId, int NumberOfRuns, int MaxAgeInMinutes,
            int regionId, double systemCostIndex, double structureBonuses, double facilityTax,
            double materialModifier,
            bool isAlphaClone)
        { 
            var blueprintList = await _blueprintManager.GetBlueprintsForActivityId(activityId);

            var i = 0;
            progressAndMax.Report(new Tuple<int, int>(i, blueprintList.Count()));
            foreach (var blueprint in blueprintList)
            {
                var prodplan = await GetProductionPlan(blueprint, new List<int> { 11 }, regionId, systemCostIndex,
                    structureBonuses, facilityTax, materialModifier, isAlphaClone);

                await CacheProductionCostAsync(prodplan, NumberOfRuns, materialModifier, MaxAgeInMinutes);
                progressAndMax.Report(new Tuple<int, int>(i++, blueprintList.Count()));
            }
            await _dbContext.SaveChangesAsync();
            progressAndMax.Report(new Tuple<int, int>(blueprintList.Count(), blueprintList.Count()));
        }

        public async Task<TableData<NormalizedProductionCost>> GetPaginatedNormalizedProductionCostAsync(string searchString, TableState state)
        {
            return await _normalizedProdcutionCostDbAccess.GetPaginatedAsync(searchString, state);
        }
    }
}
