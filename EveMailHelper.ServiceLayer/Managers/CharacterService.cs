using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.BusinessDataAccess;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class CharacterService : ICharacterService
    {
        #region injected
        private readonly IDbContextFactory<EveMailHelperContext> dbFactory = null!;
        #endregion
        private readonly EveMailHelperContext _context = null!;
        private readonly CharacterDbAccess _characterDbAccess;
        //private readonly RunnerWriteDb<ICollection<string>, ICollection<Character>> _addCharRunner;

        public CharacterService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            dbFactory = dbContextFactory;
            _context = dbFactory.CreateDbContext();
            _characterDbAccess = new CharacterDbAccess(_context);
            //_addCharRunner = new RunnerWriteDb<ICollection<string>, ICollection<Character>>
            //    (new AddCharactersAction(_characterDbAccess), dbContext);
        }

        public async Task<Character> Add(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));
            
            _characterDbAccess.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public void Delete(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));
            if (character.Id == Guid.Empty)
                throw new ArgumentException("null Guid is invalid", nameof(character));
            // now load the Evemail with all Sendto entities (child's that depend on it)
            IQueryable<Character> query = from mail in _context.Characters
                                          select mail;
            query = query.Where(mail => mail.Id == character.Id);
            var result = query.Include(mail => mail.EveMailReceived).First();

            _context.Characters.Remove(result);
            _context.SaveChanges();
        }

        public async Task Update(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
        }

        public Character GetCharactersById(Guid id)
        {
            return _characterDbAccess.GetById(id);
        }

        public ICollection<Character> GetCharactersByName(ICollection<string> characterNames)
        {
            return _characterDbAccess.GetByNames(characterNames);
        }

        public async Task<TableData<Character>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<Character> query = from character in _context.Characters
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
                //.AsNoTracking()
                .AsTracking()
                .Include(c => c.EveMailReceived)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }


    }
}
