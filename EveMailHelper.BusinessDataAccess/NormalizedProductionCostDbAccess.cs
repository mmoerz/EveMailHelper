using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess
{
    public class NormalizedProductionCostDbAccess
    {
        private readonly EveMailHelperContext _context;
        public NormalizedProductionCostDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NormalizedProductionCost prodCost)
        {
            await _context.NormalizeProductionCosts.AddAsync(prodCost);
        }

        /// <summary>
        /// adds or updates the data in the db
        /// </summary>
        /// <param name="prodCost">cost data to update</param>
        /// <returns>a tracked entity</returns>
        public async Task<NormalizedProductionCost> AddOrUpdateAsync(NormalizedProductionCost prodCost)
        {
            var found = await _context.NormalizeProductionCosts
                .Where(x => x.EveTypeId == prodCost.EveTypeId && x.ActivityId == prodCost.ActivityId)
                .AsNoTracking()
                .SingleAsync();
            if (found != null)
                await AddAsync(prodCost);
            else
                Update(prodCost);
            return prodCost;
        }

        public async Task<double> GetAgeForId(int eveId)
        {
            return await _context.NormalizeProductionCosts
                .Where(x => x.EveTypeId == eveId)
                .Select(x => DateTime.UtcNow.Subtract(x.LastUpdatedFromEve).TotalMinutes)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// don't know if this really is going to work when there is an entity already tracked
        /// </summary>
        /// <param name="prodCost"></param>
        /// <returns></returns>
        public NormalizedProductionCost Update(NormalizedProductionCost prodCost)
        {
            return _context.NormalizeProductionCosts
                .Update(prodCost)
                .Entity;
        }


        public async Task<TableData<NormalizedProductionCost>> GetPaginatedAsync(string searchString, TableState state)
        {
            IQueryable<NormalizedProductionCost> query = 
                from nprodcost in _context.NormalizeProductionCosts
                join evetype in _context.EveTypes on nprodcost.EveTypeId equals evetype.EveId
                join product in _context.EveTypes on nprodcost.ProductId equals product.EveId
                select nprodcost;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x =>
                x.EveType.TypeName.Contains(searchString) ||
                x.Product.TypeName.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "DirectCostSum" => query.OrderByDirection(state.SortDirection, x => x.DirectCostSum),
                "BestPriceSum" => query.OrderByDirection(state.SortDirection, x => x.BestPriceSum),
                "ProductCostSum" => query.OrderByDirection(state.SortDirection, x => x.ProductCostSum),
                _ => query.OrderByDirection(state.SortDirection, x => x.Product.TypeName),
            };
            var totalItems = query.Count();

            //if (state.Page > 0)
            //    query = query.Skip(state.Page * state.PageSize);
            //query = query.Take(state.PageSize);

            return new TableData<NormalizedProductionCost>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .Include(cost => cost.EveType)
                .Include(cost => cost.Product)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

    }
}
