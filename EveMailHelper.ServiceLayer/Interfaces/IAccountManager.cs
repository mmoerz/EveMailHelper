using EveMailHelper.DataModels.Security;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IAccountManager
    {
        Task<TableData<EveAccount>> GetEveAccountsPaginated(Account account, string searchString, TableState state);
        Task Remove(EveAccount eveAccount);
        Task Update(Account account);
    }
}