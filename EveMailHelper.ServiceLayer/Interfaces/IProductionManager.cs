using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IProductionManager
    {
        Task AddProductionCosts(ProductionPlan productionPlan, double systemCostIndex, double structureBonuses, double facilityTax, bool isAlphaClone);
    }
}