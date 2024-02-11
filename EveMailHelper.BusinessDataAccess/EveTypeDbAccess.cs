using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Sde.Map;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.BusinessDataAccess
{
    /// <summary>
    /// For Region, Constellation, Solarsystem access
    /// </summary>
    public class EveTypeDbAccess
    {
        private readonly EveMailHelperContext _context;
        public EveTypeDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EveType>> GetAllAsync()
        {
            return await _context.EveTypes.ToListAsync();
        }

        public async Task<EveType> GetByIdAsync(int id)
        {
            var result = await _context.EveTypes.Where(x => x.EveId == id).FirstOrDefaultAsync();
            _ = result ?? throw new NullReferenceException($"int {id} not a valid RegionId");
            return result;
        }

        public async Task<EveType> GetByNameAsync(string regionName)
        {
            var result = await _context.EveTypes.Where(x => x.TypeName == regionName).FirstOrDefaultAsync();
            _ = result ?? throw new NullReferenceException($"int {regionName} not a valid regionname");
            return result;
        }

        public async Task<IList<EveType>> SearchForName(string regionNamePart)
        {
            return await _context.EveTypes.Where(x => x.TypeName.Contains(regionNamePart)).ToListAsync();
        }

        public async Task<TableData<EveType>> GetPaginatedAsync(string searchString, TableState state)
        {
            IQueryable<EveType> query = from etype in _context.EveTypes
                                       select etype;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.TypeName.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                //"Faction" => query.OrderByDirection(state.SortDirection, x => x.Faction.Name),
                _ => query.OrderByDirection(state.SortDirection, x => x.TypeName),
            };
            var totalItems = query.Count();

            return new TableData<EveType>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .Include(etype => etype.MarketGroup)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        
    }
}
