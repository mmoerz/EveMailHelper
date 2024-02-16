using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
        Task<NormalizedProductionCost> CacheProductionCostAsync(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        NormalizedProductionCost DeriveProductionCost(ProductionPlan plan, int NumberOfRuns, double materialModifier);
        Task<ProductionPlan> GetProductionPlan(IndustryBlueprint blueprint, IList<int> activityFilerIds, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, double materialModifier, bool isAlphaClone);
    }
}