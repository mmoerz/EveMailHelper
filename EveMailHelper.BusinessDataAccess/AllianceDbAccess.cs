using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class AllianceDbAccess
    {
        private readonly EveMailHelperContext _context;
        public AllianceDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Alliance GetById(Guid id)
        {
            return _context.Alliances.Where(account => account.Id == id)
                .AsTracking()
                .Include(x => x.Corporations)
                // ?? eveaccount->characters?
                .First();
        }

        public async Task<TableData<Alliance>> GetEveAccountsPaginated(string searchString, TableState state)
        {
            IQueryable<Alliance> query = from alliance in _context.Alliances
                                         //where alliance.Id == account.Id
                                           select alliance;
            
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                    x.Name.Contains(searchString)
                    || x.Ticker != null && x.Ticker.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Ticker" => query.OrderByDirection(state.SortDirection, x => x.Ticker),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<Alliance>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .Include(x => x.Corporations)
                    .Include(x => x.Creator)
                    .Include(x => x.CreatorCorporation)
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
