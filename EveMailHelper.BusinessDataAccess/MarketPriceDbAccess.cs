using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.DataModels.Dto;

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

        public void DeleteById(int eveTypeId)
        {
            MarketPrice helper = new() { EveTypeId = eveTypeId };
            _context.Attach(helper);
            _context.MarketPrices.Remove(helper);
        }

        public async Task<ICollection<MarketPrice>> GetAllAsync()
        {
            return await _context.MarketPrices.ToListAsync();
        }

        public async Task<List<int>> GetAllIdsAsync()
        {
            var query =
                from marketprice in _context.MarketPrices
                select marketprice.EveTypeId;

            return await query.ToListAsync();
        }

        public async Task<MarketPrice> GetByIdAsync(int eveTypeid)
        {
            return await _context.MarketPrices.Where(x => x.EveTypeId == eveTypeid).SingleAsync();
        }

        public async Task<double> GetAgeForIdAsync(int eveTypeId)
        {
            return await _context.MarketPrices
                .Where(x => x.EveTypeId == eveTypeId)
                .Select(x => DateTime.UtcNow.Subtract(x.LastUpdatedFromEve).TotalMinutes)
                .SingleAsync();
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
