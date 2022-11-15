using EVEStandard.API;

using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameMailManager
    {
        Task<TableData<EVEStandard.Models.Mail>> GetInboxMails(string searchString, TableState state);
    }
}