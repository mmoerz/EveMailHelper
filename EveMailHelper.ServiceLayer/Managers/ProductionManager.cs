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

        private readonly EveMailHelperContext _dbContext;

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
        }

        public async Task<ProductionPlan> GetProductionPlan(
            IndustryBlueprint blueprint, IList<int> activityFilerIds,
            int regionId, double systemCostIndex, double structureBonuses, double facilityTax,
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
                plan.ProductPricePerUnit = sellbuyPrice.SellPrice;
                foreach (var item in plan)
                {
                    sellbuyPrice = await _marketManager.ArchivedBuySellPrice(
                        regionId, item.EveType.EveId, MaxAgeInMinutes);
                    item.PricePerUnit = sellbuyPrice.SellPrice;
                }
                await AddProductionCosts(plan, systemCostIndex, structureBonuses, facilityTax, isAlphaClone);
            }
            return plan;
        }

        // handover
        // Buildplan with costs, system ??, station ??, time to run
        // add additional costs to buildplan

        public async Task AddProductionCosts(
            ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax,
            bool isAlphaClone)
        {
            productionPlan.JobCost = await JobCost(productionPlan, systemCostIndex, structureBonuses,
                facilityTax, isAlphaClone);
            foreach (var component in productionPlan.SubComponents)
            {
                await Produce(component, systemCostIndex, structureBonuses, facilityTax, isAlphaClone);
            }
        }

        protected async Task Produce(
            BlueprintComponent component, double systemCostIndex, double structureBonuses, double facilityTax,
            bool isAlphaClone)
        {
            // only if subcomponents are present, we continue
            if (!component.SubComponents.Any())
                return;
            // buy or produce?
            if (component.IsBuyingBetter)
                return;
            // get job costs and store them in the component tree
            component.JobCost = await JobCost(component, systemCostIndex, structureBonuses, facilityTax, isAlphaClone);

            foreach (var subcomponent in component.SubComponents)
            {
                await Produce(subcomponent, systemCostIndex, structureBonuses, facilityTax, isAlphaClone);
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
            IBlueprintComponentTree component, double systemCostIndex, double structureBonuses, double facilityTax,
            bool isAlphaClone)
        {
            double totalJobCost = 0;
            double estimatedItemValue = 0;
            foreach (var subcomponent in component.SubComponents)
            {
                var marketPrice =
                    await _marketManager.GetMarketPrice(subcomponent.EveType.EveId, MaxAgeInMinutes);
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

        public BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns)
        {
            BuyList buyList = new()
            {
                Name = plan.ProductName,
                NumberOfRuns = NumberOfRuns,
            };

            // check if it's cost effective to build
            if (plan.IsProducingBetter)
            {
                int minimumNumberOfRuns;
                if (!NumberOfRunsIsValid(plan, NumberOfRuns, out minimumNumberOfRuns))
                    throw new Exception($"Number of Runs must be a multiple of {minimumNumberOfRuns}");
                foreach (var component in plan.SubComponents)
                {
                    buyList.Merge(RecursiveBestPriceBuyList(component, NumberOfRuns));
                }
            }

            return buyList;
        }

        public bool NumberOfRunsIsValid(ProductionPlan plan, int NumberOfRuns, out int minimumNumberOfRuns)
        {
            minimumNumberOfRuns = plan.GetMinNumberOfRuns();
            var modulo = NumberOfRuns % minimumNumberOfRuns;
            if (modulo > 0)
                return false;
            return true;
        }

        protected BuyList RecursiveBestPriceBuyList(BlueprintComponent component, int NumberOfRuns)
        {
            BuyList buyList = new();
            if (component.IsBuyingBetter)
            {
                buyList.ItemList.Add(new()
                {
                    EveType = component.EveType,
                    Quantity = component.Quantity * NumberOfRuns,
                }
                );
                return buyList;
            }

            int subComponentsNumberOfRuns = (int)(NumberOfRuns / component.ForcedQuantityMultiplier);
            foreach (var subComponent in component.SubComponents)
            {
                buyList.Merge(RecursiveBestPriceBuyList(subComponent, subComponentsNumberOfRuns));
            }
            return buyList;
        }

        public class ProductionCost
        {
            public double JobCost;
            public double ComponentCost;
        }

        public async Task<NormalizeProductionCost> CacheProductionCostAsync(ProductionPlan plan, int NumberOfRuns)
        {
            //Todo: check if the db already contains this data ??

            var result = DeriveProductionCost(plan, NumberOfRuns);
            return await _normalizedProdcutionCostDbAccess.AddOrUpdateAsync(result);
        }

        // Direct build using the top level blueprint and it's required components
        // Bestprice build - replacing buying components by building them if this is cheaper
        // compare to quantity * current marketprice
        public NormalizeProductionCost DeriveProductionCost(ProductionPlan plan, int NumberOfRuns)
        {
            _ = plan.Product ?? throw new Exception("ProductionPlan with empty product");
            NormalizeProductionCost result = new()
            {
                // MUST BE SET!!!
                EveType = plan.Activity.Type, // connection to activity
                IndustryActivity = plan.Activity,
                NumberOfRuns = NumberOfRuns,
                Product = plan.Product,
                ProductQuantity = plan.ProductQuantity,
                ProductPricePerUnit = plan.ProductPricePerUnit,
                DirectJobCost = plan.JobCost,
            };

            foreach (var subComponent in plan.SubComponents)
            {
                result.DirectComponentCost += subComponent.PriceSum;
            }
            // here comes the complex part, selecting the best price articles
            // we are aggregating information (throwing away details about how the price was calculated)
            result.BestPriceJobCost = result.DirectJobCost;
            result.BestPriceComponentCost = result.BestPriceComponentCost;
            if (plan.IsProducingBetter)
            {
                int minimumNumberOfRuns;
                if (!NumberOfRunsIsValid(plan, NumberOfRuns, out minimumNumberOfRuns))
                    throw new Exception($"Number of Runs must be a multiple of {minimumNumberOfRuns}");
                result.BestPriceJobCost = 0;
                result.BestPriceComponentCost = 0;
                foreach (var component in plan.SubComponents)
                {
                    var subresult = RecursiveBestPriceProductionCost(component, NumberOfRuns);
                    result.BestPriceJobCost += subresult.JobCost;
                    result.BestPriceComponentCost += subresult.ComponentCost;
                }
            }

            return result;
        }

        protected ProductionCost RecursiveBestPriceProductionCost(
            BlueprintComponent component, int NumberOfRuns)
        {
            var result = new ProductionCost();
            BuyList buyList = new();
            if (component.IsBuyingBetter)
            {
                result.ComponentCost = component.PriceSum * NumberOfRuns;
                return result;
            }

            result.JobCost += component.JobCost;
            int subComponentsNumberOfRuns = (int)(NumberOfRuns / component.ForcedQuantityMultiplier);
            foreach (var subComponent in component.SubComponents)
            {
                var subresult = RecursiveBestPriceProductionCost(component, NumberOfRuns);
                result.JobCost += subresult.JobCost;
                result.ComponentCost += subresult.ComponentCost;
            }
            return result;
        }
    }
}
