﻿using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class CorporationDbAccess
    {
        private readonly EveMailHelperContext _context;
        public CorporationDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Corporation GetById(Guid id)
        {
            return _context.Corporations
                .Where(account => account.Id == id)
                .AsTracking()
                .Include(x => x.Alliance)
                .Include(x => x.Ceo)
                // ?? eveaccount->characters?
                .First();
        }

        public ICollection<Corporation> GetByEveIds(ICollection<int> ids)
        {
            return _context.Corporations
                .Where(account => ids.Contains(account.EveId))
                .AsTracking()
                .Include(x => x.Alliance)
                .Include(x => x.Ceo)
                // ?? eveaccount->characters?
                .ToList();
        }

        public async Task<TableData<Corporation>> GetEveAccountsPaginated(string searchString, TableState state)
        {
            IQueryable<Corporation> query = from alliance in _context.Corporations
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

            return new TableData<Corporation>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .Include(x => x.Alliance)
                    .Include(x => x.Ceo)
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }

        public Corporation Add(Corporation item)
        {
            return _context.Corporations.Add(item)
                .Entity;
        }

        public void Update(Corporation item)
        {
            _context.Corporations.Update(item);
        }

        public void Remove(Corporation item)
        {
            _context.Corporations.Remove(item);
        }
    }
}