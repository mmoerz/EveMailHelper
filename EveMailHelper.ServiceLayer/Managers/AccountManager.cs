using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly EveMailHelperContext _dbContext;
        private readonly AccountDbAccess _accountDbAccess;
        private readonly EveAccountDbAccess _eveaccountDbAccess;
        private readonly CharacterDbAccess _characterDbAccess;

        public AccountManager(
           IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _accountDbAccess = new(_dbContext);
            _eveaccountDbAccess = new(_dbContext);
            _characterDbAccess = new(_dbContext);
        }

        public async Task Update(Account account)
        {
            _accountDbAccess.Update(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(EveAccount account)
        {
            _eveaccountDbAccess.Update(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(EveAccount eveAccount)
        {
            if (eveAccount.Characters.Count > 0)
                throw new Exception("cannot delete EveAccount with characters");
            _eveaccountDbAccess.Remove(eveAccount);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TableData<EveAccount>> GetEveAccountsPaginated(
            Account account, string searchString, TableState state)
        {
            return await _accountDbAccess.GetEveAccountsPaginated(account, searchString, state);
        }

        public async Task<TableData<Character>> GetCharactersPaginated(
            Account account, EveAccount eveAccount, string searchString, TableState state)
        {
            return await _characterDbAccess.GetCharactersPaginated(account, eveAccount, searchString, state);
        }
    }
}
