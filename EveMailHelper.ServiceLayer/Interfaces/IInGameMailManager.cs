using MudBlazor;
using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameMailManager
    {
        Task<TableData<Mail>> GetInboxMails(string searchString, TableState state);
    }
}