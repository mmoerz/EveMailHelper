using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.BusinessDataAccess
{
    public class BlueprintDbAccess
    {
        private readonly EveMailHelperContext _context;
        public BlueprintDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public void Delete(IndustryBlueprint blueprint)
        {
            _context.IndustryBlueprints.Remove(blueprint);
        }

        public async Task<ICollection<IndustryBlueprint>> GetAllAsync()
        {
            return await _context.IndustryBlueprints.ToListAsync();
        }

        public async Task<IndustryBlueprint?> GetByIdAsync(int EveId)
        {
            return await _context.IndustryBlueprints.Where(x => x.TypeId == EveId).FirstOrDefaultAsync();
        }

        public async Task<IndustryBlueprint?> GetByNameAsync(string blueprintName)
        {
            return await _context.IndustryBlueprints.Where(x => x.Type.TypeName == blueprintName)
                .FirstOrDefaultAsync();
        }

        public async Task<TableData<IndustryBlueprint>> GetPaginatedAsync(
            string grpFilter, string searchString, TableState state)
        {
            IQueryable<IndustryBlueprint> query;

            query = from blueprint in _context.IndustryBlueprints
                    join type in _context.EveTypes on blueprint.TypeId equals type.EveId
                    join grp in _context.Groups on type.GroupId equals grp.EveId
                    select blueprint;

            query = query.Where(
                x => x.Type.TypeName.Contains(searchString) 
                || (x.Type.Group != null && x.Type.Group.GroupName.Contains(searchString))
                );
            if (!string.IsNullOrEmpty(grpFilter))
                query = query.Where(x => (x.Type.Group != null && x.Type.Group.GroupName.Contains(grpFilter)));
            
            query = state.SortLabel switch
            {
                "Content" => query.OrderByDirection(state.SortDirection, x => x.Type.TypeName),
                //"Group" => query.OrderByDirection(state.SortDirection, x => x.Type.Group.GroupName),
                //"Marketgroup" => query.OrderByDirection(state.SortDirection, x => x.Type.MarketGroup.MarketGroupName)
                _ => query.OrderByDirection(state.SortDirection, x => x.Type.TypeName),
            };
            var totalItems = query.Count();

            //if (state.Page > 0)
            //    query = query.Skip(state.Page * state.PageSize);
            //query = query.Take(state.PageSize);

            return new TableData<IndustryBlueprint>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .Include(IndustryBlueprint => IndustryBlueprint.Type)
                .ThenInclude(evetype => evetype.Group)
                .Include(blueprint =>  blueprint.Type)
                .ThenInclude(evetype => evetype.MarketGroup)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public void Add(IndustryBlueprint blueprint)
        {
            _context.IndustryBlueprints.Add(blueprint);
        }

        public void Update(IndustryBlueprint blueprint)
        {
            _context.IndustryBlueprints.Update(blueprint);
        }
    }
}
