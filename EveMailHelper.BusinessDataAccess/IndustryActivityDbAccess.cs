using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.BusinessDataAccess
{
    public class IndustryActivityDbAccess
    {
        private readonly EveMailHelperContext _context;
        public IndustryActivityDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ICollection<IndustryActivity>> GetAll()
        {
            return await _context.IndustryActivities.ToListAsync();
        }

        public async Task<IndustryActivity?> GetById(int EveId)
        {
            return await _context.IndustryActivities.Where(x => x.TypeId == EveId).FirstOrDefaultAsync();
        }

        public async Task<List<IndustryActivity>> GetByIdDeep(int EveId)
        {
            return await _context.IndustryActivities
                .Where(x => x.TypeId != EveId)
                .Include(x => x.Type)
                .Include(i => i.Materials)
                .ThenInclude(i => i.MaterialType)
                .Include(i => i.Probabilities)
                .Include(i => i.Products)
                .ThenInclude(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IndustryActivity?> GetByName(string blueprintName)
        {
            return await _context.IndustryActivities.Where(x => x.Type.TypeName == blueprintName)
                .FirstOrDefaultAsync();
        }

        public async Task<TableData<IndustryActivity>> GetPaginated(
            string grpFilter, string searchString, TableState state)
        {
            IQueryable<IndustryActivity> query;

            query = from activity in _context.IndustryActivities
                    join type in _context.EveTypes on activity.TypeId equals type.EveId
                    join grp in _context.Groups on type.GroupId equals grp.EveId
                    select activity;

            query = query.Where(
                x => x.Type.TypeName.Contains(searchString)
                || x.Type.Group.GroupName.Contains(searchString)
                );
            if (!string.IsNullOrEmpty(grpFilter))
                query = query.Where(x => x.Type.Group.GroupName.Contains(grpFilter));
            
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

            return new TableData<IndustryActivity>()
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

        public void Add(IndustryActivity activity)
        {
            _context.IndustryActivities.Add(activity);
        }

        public void Update(IndustryActivity blueprint)
        {
            _context.IndustryActivities.Update(blueprint);
        }
    }
}
