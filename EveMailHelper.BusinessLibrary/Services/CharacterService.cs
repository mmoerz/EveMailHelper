using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dbAccess;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class CharacterService : ICharacterService
    {
        #region injected
        private readonly IDbContextFactory<EveMailHelperContext> dbFactory = null!;
        #endregion
        private readonly EveMailHelperContext dbContext = null!;
        private readonly CharacterDbAccess _characterDbAccess;
        //private readonly RunnerWriteDb<ICollection<string>, ICollection<Character>> _addCharRunner;

        public CharacterService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            dbFactory = dbContextFactory;
            dbContext = dbFactory.CreateDbContext();
            _characterDbAccess = new CharacterDbAccess(dbContext);
            //_addCharRunner = new RunnerWriteDb<ICollection<string>, ICollection<Character>>
            //    (new AddCharactersAction(_characterDbAccess), dbContext);
        }

        public ICollection<Character> GetCharactersByName(ICollection<string> characterNames)
        {
            return _characterDbAccess.GetCharactersByName(characterNames);
        }

        public async Task<TableData<Character>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<Character> query = from character in dbContext.Characters
                                          select character;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Description" => query.OrderByDirection(state.SortDirection, x => x.Description),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            if (state.Page > 0)
                query = query.Skip(state.Page * state.PageSize);
            query = query.Take(state.PageSize);

            return new TableData<Character>()
            {
                Items = await query
                .AsNoTracking()
                .Include(c => c.EveMailReceived)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }


    }
}
