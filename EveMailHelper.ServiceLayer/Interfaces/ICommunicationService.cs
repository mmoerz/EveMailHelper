using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.BusinessLibrary.Complex.dto;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface ICommunicationService
    {
        Task<TableData<Communication>> GetPaginated(Character character, string searchString, TableState state);
        Task<TableData<EveMail>> GetPaginatedEveMail(Character character, string searchString, TableState state);
    }
}