using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface IEveMailService
    {
        EveMail Update(EveMail eveMail);
        Task<TableData<EveMail>> GetPaginated(string searchString, TableState state);
        Task SendTo(Guid templateId, ICollection<string> receivers);
    }
}