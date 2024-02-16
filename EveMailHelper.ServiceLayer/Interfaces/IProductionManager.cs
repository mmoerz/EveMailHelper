using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
        BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        NormalizedProductionCost DeriveProductionCost(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        Task<TableData<NormalizedProductionCost>> GetPaginatedNormalizedProductionCostAsync(string searchString, TableState state);
        Task<ProductionPlan> GetProductionPlan(IndustryBlueprint blueprint, IList<int> activityFilerIds, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
        Task PreprocessBlueprintsForActivity(int activityId, int NumberOfRuns, int MaxAgeInMinutes, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
    }
}