using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface IEveMailTemplateService
    {
        Task<EveMailTemplate> AddOrUpdate(EveMailTemplate eveMail);
        Task<TableData<EveMailTemplate>> GetPaginated(string searchString, TableState state);
    }
}