using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.BusinessDataAccess
{
    public class MarketOrderDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MarketOrderDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public void Delete(MarketOrder marketOrder)
        {
            _context.MarketOrders.Remove(marketOrder);
        }

        public void DeleteById(long orderId)
        {
            MarketOrder helper = new() { EveId = orderId };
            _context.Attach(helper);
            _context.MarketOrders.Remove(helper);
        }

        public async Task<ICollection<MarketOrder>> GetAllAsync()
        {
            return await _context.MarketOrders.ToListAsync();
        }
        
        public async Task<MarketOrder?> GetByIdAsync(int orderId)
        {
            return await _context.MarketOrders.Where(x => x.EveId == orderId).SingleAsync();
        }

        public async Task<List<MarketOrder>> GetByTypeIdAsync(int eveTypeId)
        {
            return await _context.MarketOrders.Where(x => x.TypeId == eveTypeId).ToListAsync();
        }

        public List<long> GetIdsForEveType(int eveTypeId)
        {
            return _context.MarketOrders
                .Where(x => x.TypeId == eveTypeId)
                .Select(c => c.EveId)
                .ToList();
        }

        public async Task<TableData<MarketOrder>> GetTypePaginatedAsync(
            int eveTypeId, string searchString, TableState state, bool? isBuyOrder)
        {
            IQueryable<MarketOrder> query = 
                from marketorder in _context.MarketOrders
                join solarsystem in _context.SolarSystems
                    on marketorder.SolarSystemId equals solarsystem.EveId
                where marketorder.TypeId == eveTypeId
                where (isBuyOrder != null && marketorder.IsBuyOrder == isBuyOrder)
                select marketorder;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                x.SolarSystem.Name.Contains(searchString)
                );
            }

            query = state.SortLabel switch
            {
                "BuySell" => query.OrderByDirection(state.SortDirection, x => x.IsBuyOrder),
                "Price" =>  query.OrderByDirection(state.SortDirection, x => x.Price),
                "SolarSystem" => query.OrderByDirection(state.SortDirection, x => x.SolarSystem.Name),
                _ => query.OrderByDirection(state.SortDirection, x => x.Price),
            };
            var totalItems = query.Count();

            return new TableData<MarketOrder>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public void Add(MarketOrder marketorder)
        {
            _context.MarketOrders.Add(marketorder);
        }

        public void Update(MarketOrder marketorder)
        {
            _context.MarketOrders.Update(marketorder);
        }
    }
}
