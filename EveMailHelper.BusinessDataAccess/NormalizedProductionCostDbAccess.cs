using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels.Market;

using Microsoft.EntityFrameworkCore;

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

        public async Task AddAsync(NormalizeProductionCost prodCost)
        {
            await _context.NormalizeProductionCosts.AddAsync(prodCost);
        }

        /// <summary>
        /// adds or updates the data in the db
        /// </summary>
        /// <param name="prodCost">cost data to update</param>
        /// <returns>a tracked entity</returns>
        public async Task<NormalizeProductionCost> AddOrUpdateAsync(NormalizeProductionCost prodCost)
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

        /// <summary>
        /// don't know if this really is going to work when there is an entity already tracked
        /// </summary>
        /// <param name="prodCost"></param>
        /// <returns></returns>
        public NormalizeProductionCost Update(NormalizeProductionCost prodCost)
        {
            return _context.NormalizeProductionCosts
                .Update(prodCost)
                .Entity;
        }


    }
}
