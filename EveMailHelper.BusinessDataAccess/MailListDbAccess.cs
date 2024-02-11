using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class MailListDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MailListDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public MailList GetById(Guid id)
        {
            return _context.MailLists.Where(account => account.Id == id)
                .AsTracking()
                .First();
        }

        public ICollection<MailList> GetByEveIds(ICollection<int> ids)
        {
            return _context.MailLists
                .Where(account => ids.Contains(account.EveId))
                .AsTracking()
                .ToList();
        }

        public async Task<TableData<MailList>> GetEveAccountsPaginatedAsync(string searchString, TableState state)
        {
            IQueryable<MailList> query = from alliance in _context.MailLists
                                             //where alliance.Id == account.Id
                                         select alliance;

            if (!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(x => x.Name.Contains(searchString));

            query = state.SortLabel switch
            {
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<MailList>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }

        public MailList Add(MailList item)
        {
            return _context.MailLists.Add(item)
                .Entity;
        }

        public void Update(MailList item)
        {
            _context.MailLists.Update(item);
        }

        public void Remove(MailList item)
        {
            _context.MailLists.Remove(item);
        }
    }
}
