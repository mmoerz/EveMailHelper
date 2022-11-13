using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IAccountManager
    {
        Task<TableData<Character>> GetCharactersPaginated(Account account, EveAccount eveAccount, string searchString, TableState state);
        Task<TableData<EveAccount>> GetEveAccountsPaginated(Account account, string searchString, TableState state);
        Task Remove(EveAccount eveAccount);
        Task Update(Account account);
        Task Update(EveAccount account);
    }
}