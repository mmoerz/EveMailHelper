using EveMailHelper.DataModels.Sde;
using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IBlueprintManager
    {
        Task<TableData<IndustryBlueprint>> GetBlueprintsPaginated(string searchString, TableState state);
    }
}