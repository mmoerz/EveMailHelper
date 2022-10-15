using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface IEveMailService
    {
        Task<EveMail> AddOrUpdate(EveMail eveMail);
        Task<TableData<EveMail>> GetPaginated(string searchString, TableState state);
    }
}