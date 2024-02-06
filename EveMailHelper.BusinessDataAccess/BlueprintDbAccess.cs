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

        public async Task<ICollection<IndustryBlueprint>> GetAll()
        {
            return await _context.IndustryBlueprints.ToListAsync();
        }

        public async Task<IndustryBlueprint?> GetById(int EveId)
        {
            return await _context.IndustryBlueprints.Where(x => x.TypeId == EveId).FirstOrDefaultAsync();
        }

        public async Task<IndustryBlueprint?> GetByName(string blueprintName)
        {
            return await _context.IndustryBlueprints.Where(x => x.Type.TypeName == blueprintName)
                .FirstOrDefaultAsync();
        }

        public async Task<TableData<IndustryBlueprint>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<IndustryBlueprint> query;

            if (!string.IsNullOrWhiteSpace(searchString))
                query = from blueprint in _context.IndustryBlueprints
                        join type in _context.EveTypes on blueprint.TypeId equals type.EveId
                        where type.TypeName.Contains(searchString)
                        select blueprint;
            else
                query = from blueprint in _context.IndustryBlueprints
                        join type in _context.EveTypes on blueprint.TypeId equals type.EveId
                        select blueprint;

            /*
            from person in _dbContext.Person
            join detail in _dbContext.PersonDetails on person.Id equals detail.PersonId into Details
            from m in Details.DefaultIfEmpty()
            select new
            {
                id = person.Id,
                firstname = person.Firstname,
                lastname = person.Lastname,
                detailText = m.DetailText
            };
            */
            

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
