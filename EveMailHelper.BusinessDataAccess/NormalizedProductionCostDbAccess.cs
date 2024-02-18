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
                .Where(x => x.EveTypeId == prodCost.EveType.EveId && x.ActivityId == prodCost.IndustryActivity.ActivityId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            prodCost.EveTypeId = prodCost.EveType.EveId;
            prodCost.ActivityId = prodCost.IndustryActivity.ActivityId;
            prodCost.ProductId = prodCost.Product.EveId;
            prodCost.EveType = null;
            prodCost.Product = null;
            prodCost.IndustryActivity = null;

            if (found != null)
                Update(prodCost); 
            else
                await AddAsync(prodCost);
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


        public async Task<IList<NormalizedProductionCost>> GetAsync(string searchString)
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

            return await query
                .AsNoTracking()
                .Include(cost => cost.EveType)
                .Include(cost => cost.Product)
                .Include(cost => cost.IndustryActivity)
                .ToListAsync();
        }

    }
}
