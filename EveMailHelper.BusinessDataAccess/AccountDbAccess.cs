using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class AccountDbAccess
    {
        private readonly EveMailHelperContext _context;
        public AccountDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Account GetById(Guid id)
        {
            return _context.Accounts.Where(account => account.Id == id)
                .AsTracking()
                .Include(x => x.EveAccounts)
                // ?? eveaccount->characters?
                .Include(x => x.Characters)
                .First();
        }

        public async Task<TableData<EveAccount>> GetEveAccountsPaginated(Account account, string searchString, TableState state)
        {
            IQueryable<EveAccount> query = from eveaccount in _context.EveAccounts
                                           where eveaccount.Id == account.Id
                                           select eveaccount;
            
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                    x.Name.Contains(searchString)
                    || x.Description != null && x.Description.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Description" => query.OrderByDirection(state.SortDirection, x => x.Description),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<EveAccount>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .Include(x => x.Account)
                    .Include(x => x.Characters)
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
        }

        public void Remove(Account eveAccount)
        {
            _context.Accounts.Remove(eveAccount);
        }
    }
}
