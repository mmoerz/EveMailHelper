﻿using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
        BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        ProductionCostExtended DeriveProductionCostWithTax(ProductionPlan plan, int NumberOfRuns, double materialModifier, int AccountSkillLevel, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        Task<TableData<ProductionCostExtended>> GetPaginatedNormalizedProductionCostAsync(string searchString, TableState state, int AccountSkillLevel, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        Task<ProductionPlan> GetProductionPlan(IndustryBlueprint blueprint, IList<int> activityFilerIds, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
        Task PreprocessBlueprintsForActivity(IProgress<Tuple<int, int>> progressAndMax, int activityId, int NumberOfRuns, int MaxAgeInMinutes, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
    }
}