using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface ICommunicationService
    {
        Task<TableData<Communication>> GetPaginated(Character character, string searchString, TableState state);
        Task<TableData<EveMail>> GetPaginatedEveMail(Character character, string searchString, TableState state);
    }
}