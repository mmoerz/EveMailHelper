using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.ServiceLibrary.Managers;

using EveNatTools.ServiceLibrary.Utilities;

using EVEStandard;
using EVEStandard.Models.API;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public class ProductionManager : IProductionManager
    {
        private readonly IMarketManager _marketManager;

        private readonly EveMailHelperContext _dbContext;

        private readonly MapDbAccess _mapDbAccess;

        private int MaxAgeInMinutes = 60;

        /// <summary>
        /// fixed at 4%
        /// </summary>
        private readonly double SCCsurcharge = 0.04;
        /// <summary>
        /// Only applicable to alpha clones, set to 0.25%
        /// </summary>
        private readonly double AlphaCloneTax = 0.0025;

        public ProductionManager(
            IMarketManager marketManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory
            )
        {
            _marketManager = marketManager;
            _dbContext = dbContextFactory.CreateDbContext();
            _mapDbAccess = new MapDbAccess(_dbContext);

        }

        // handover
        // Buildplan with costs, system ??, station ??, time to run
        // add additional costs to buildplan

        public async Task AddProductionCosts(
            ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax)
        {
            foreach (var component in productionPlan.SubComponents)
            {
                await Produce(component, systemCostIndex, structureBonuses, facilityTax);
            }
        }

        protected async Task Produce(
            BlueprintComponent component, double systemCostIndex, double structureBonuses, double facilityTax)
        {
            // only if subcomponents are present, we continue
            if (!component.SubComponents.Any())
                return;
            // buy or produce?
            if (component.IsBuyingBetter)
                return;
            // get job costs and store them in the component tree
            component.JobCost = await JobCost(component, systemCostIndex, structureBonuses, facilityTax);

            foreach (var subcomponent in component.SubComponents)
            {
                await Produce(subcomponent, systemCostIndex, structureBonuses, facilityTax);
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
            BlueprintComponent component, double systemCostIndex, double structureBonuses, double facilityTax)
        {
            double totalJobCost = 0;
            double estimatedItemValue = 0;
            foreach (var subcomponent in component.SubComponents)
            {
                var marketPrice = await _marketManager.GetMarketPrice(subcomponent.EveId, MaxAgeInMinutes);
                estimatedItemValue += subcomponent.Quantity * marketPrice.AdjustedPrice;
            }

            totalJobCost = estimatedItemValue *
                ((systemCostIndex * structureBonuses) * facilityTax * SCCsurcharge * AlphaCloneTax);
            return totalJobCost;
        }
    }
}
