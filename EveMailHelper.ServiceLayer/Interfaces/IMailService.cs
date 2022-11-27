using EveMailHelper.DataModels;
using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IMailService
    {
        Mail Update(Mail eveMail);
        Task<TableData<Mail>> GetPaginated(string searchString, TableState state);
        Task SendTo(Guid templateId, Character fromCharacter, ICollection<string> receivers);
        void Delete(Mail eveMail);
        Task<List<string>> FilterReceivers(string receivers, DateTime filterTime);
        Task<TableData<Mail>> GetPaginatedCurrentCharacter(string searchString, TableState state);
        Task<TableData<Mail>> GetPaginated(Character fromCharacter, string searchString, TableState state);
    }
}