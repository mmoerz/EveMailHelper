using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Models;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IBlueprintManager
    {
        Task<List<IndustryActivity>> GetBlueprintActivity(int EveId);
        Task<ProductionPlan> GetBlueprintComponentsList(IndustryBlueprint blueprint, int filterActivity = 0);
        Task<List<IndustryBlueprint>> GetBlueprintsForActivityId(int activityId);
        Task<TableData<IndustryBlueprint>> GetBlueprintsPaginated(string groupFilter, string searchString, TableState state);
    }
}