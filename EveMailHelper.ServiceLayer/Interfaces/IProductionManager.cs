using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, bool isAlphaClone);
        BuyList DeriveBestPriceBuyListFromPlan(ProductionPlan plan, int NumberOfRuns);
        Task<ProductionPlan> GetProductionPlan(IndustryBlueprint blueprint, IList<int> activityFilerIds, int regionId, double systemCostIndex, double structureBonuses, double facilityTax, bool isAlphaClone);
    }
}