using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.DataModels.Dto;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.BusinessDataAccess
{
    public class MarketPriceDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MarketPriceDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public void Delete(MarketPrice marketprice)
        {
            _context.MarketPrices.Remove(marketprice);
        }

        /// <summary>
        /// deletes given Id very fast, but will only work if id is not tracked yet
        /// </summary>
        /// <param name="eveTypeId"></param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int eveTypeId)
        {
            // best performance in .net core 6.0
            // but does not work if there is already an object attached with that id
            MarketPrice helper = new() { EveTypeId = eveTypeId };
            _context.Attach(helper);
            // this is the safe way to remove the id
            //var helper = await _context.MarketPrices.SingleAsync(x => x.EveTypeId == eveTypeId);
            _context.MarketPrices.Remove(helper);
        }

        public async Task<ICollection<MarketPrice>> GetAllAsync()
        {
            return await _context.MarketPrices
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<int>> GetAllIdsAsync()
        {
            return await _context.MarketPrices
                .AsNoTracking()
                .Select(x => x.EveTypeId)
                .ToListAsync();
        }

        public async Task<MarketPrice> GetByIdAsync(int eveTypeId)
        {
            return await _context.MarketPrices
                .Where(x => x.EveTypeId == eveTypeId)
                .AsNoTracking()
                .SingleAsync();
        }

        public async Task<double> GetAgeForIdAsync(int eveTypeId)
        {
            try
            {
                //var query = from marketprice in _context.MarketPrices
                //            where marketprice.EveTypeId == eveTypeId
                //            select marketprice;
                ////select DateTime.UtcNow.Subtract(marketprice.LastUpdatedFromEve).TotalMinutes;
                //var help = await _context.MarketPrices.Where(x => x.EveTypeId == eveTypeId).SingleAsync();
                
                //age = DateTime.UtcNow.Subtract(help.LastUpdatedFromEve).TotalMinutes;

                return await _context.MarketPrices
                    .Where(x => x.EveTypeId == eveTypeId)
                    .AsNoTracking()
                    .Select(x => DateTime.UtcNow.Subtract(x.LastUpdatedFromEve).TotalMinutes)
                    .SingleAsync();
            } catch 
            {
                // rejoice this is just an empty database
            }
            return 0.0;
        }

        public void Add(MarketPrice marketprice)
        {
            _context.MarketPrices.Add(marketprice);
        }

        public void Update(MarketPrice marketprice)
        {
            _context.MarketPrices.Update(marketprice);
        }
    }
}
