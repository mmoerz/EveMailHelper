using MudBlazor;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IEveMailTemplateService
    {
        EveMailTemplate AddOrUpdate(EveMailTemplate eveMail);
        Task<ICollection<EveMailTemplate>> GetAll();
        Task<EveMailTemplate?> GetById(Guid id);
        Task<TableData<EveMailTemplate>> GetPaginated(string searchString, TableState state);
    }
}