using MudBlazor;
using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameMailManager
    {
        /// <summary>
        /// Downloads all Inbox Mails for the current character
        /// </summary>
        /// <returns></returns>
        Task GetInboxMails();
        //Task<TableData<Mail>> GetPaginated(Character fromCharacter, string searchString, TableState state);
        //Task<TableData<Mail>> GetPaginatedCurrentCharacter(string searchString, TableState state);
    }
}