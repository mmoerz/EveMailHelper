using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessDataAccess
{
    public class EveMailTemplateDbAccess
    {
        private readonly EveMailHelperContext _context;
        public EveMailTemplateDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EveMailTemplate>> GetAll()
        {
            return await _context.EveMailTemplates.ToListAsync();
        }

        public async Task<EveMailTemplate?> GetById(Guid id)
        {
            return await _context.EveMailTemplates.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TableData<EveMailTemplate>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<EveMailTemplate> query = from mail in _context.EveMailTemplates
                                                select mail;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.Subject.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Content" => query.OrderByDirection(state.SortDirection, x => x.Content),
                _ => query.OrderByDirection(state.SortDirection, x => x.Subject),
            };
            var totalItems = query.Count();

            if (state.Page > 0)
                query = query.Skip(state.Page * state.PageSize);
            query = query.Take(state.PageSize);

            return new TableData<EveMailTemplate>()
            {
                Items = await query
                .AsNoTracking()
                .Include(emt => emt.EveMailsGenerated)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public void Add(EveMailTemplate template)
        {
            _context.EveMailTemplates.Add(template);
        }

        public void Update(EveMailTemplate template)
        {
            _context.EveMailTemplates.Update(template);
        }
    }
}
