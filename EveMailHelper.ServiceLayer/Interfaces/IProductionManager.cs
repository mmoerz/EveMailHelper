using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, bool isAlphaClone);
        Task<NormalizeProductionCost> CacheProductionCostAsync(ProductionPlan plan, int NumberOfRuns);
        BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns);
        NormalizeProductionCost DeriveProductionCost(ProductionPlan plan, int NumberOfRuns);
        Task<ProductionPlan> GetProductionPlan(IndustryBlueprint blueprint, IList<int> activityFilerIds, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, bool isAlphaClone);
    }
}