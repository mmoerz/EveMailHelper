using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.BusinessDataAccess
{
    /// <summary>
    /// For Region, Constellation, Solarsystem access
    /// </summary>
    public class MapDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MapDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Region>> GetAllRegions()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionById(int id)
        {
            var result = await _context.Regions.Where(x => x.EveId == id).FirstOrDefaultAsync();
            _ = result ?? throw new NullReferenceException($"int {id} not a valid RegionId");
            return result;
        }

        public async Task<Region> GetRegionByName(string regionName)
        {
            var result = await _context.Regions.Where(x => x.Name == regionName).FirstOrDefaultAsync();
            _ = result ?? throw new NullReferenceException($"int {regionName} not a valid regionname");
            return result;
        }

        public async Task<IList<Region>> SearchForRegionName(string regionNamePart)
        {
            return await _context.Regions.Where(x => x.Name.Contains(regionNamePart)).ToListAsync();
        }

        public async Task<TableData<Region>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<Region> query = from reg in _context.Regions
                                       join faction in _context.Factions 
                                                on reg.FactionId equals faction.EveId
                                       select reg;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                    x.Name.Contains(searchString) || 
                    (x.Faction != null && x.Faction.Name.Contains(searchString))
                );
            }

            query = state.SortLabel switch
            {
                "Faction" => query.OrderByDirection(state.SortDirection, x => x.Faction.Name),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<Region>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .Include(reg => reg.Faction)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        
    }
}
