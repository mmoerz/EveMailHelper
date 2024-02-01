using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface IEveMailTemplateService
    {
        EveMailTemplate AddOrUpdate(EveMailTemplate eveMail);
        Task<ICollection<EveMailTemplate>> GetAll();
        Task<EveMailTemplate?> GetById(Guid id);
        Task<TableData<EveMailTemplate>> GetPaginated(string searchString, TableState state);
    }
}