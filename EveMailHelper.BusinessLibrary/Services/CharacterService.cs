using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class CharacterService : ICharacterService
    {
        #region injected
        private readonly IDbContextFactory<EveMailHelperContext> dbFactory = null!;
        #endregion
        private readonly EveMailHelperContext dbContext = null!;

        public CharacterService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            dbFactory = dbContextFactory;
            dbContext = dbFactory.CreateDbContext();
        }

        public ICollection<Character> GetCharactersByName(ICollection<string> characterNames)
        {
            IQueryable<Character> query = from character in dbContext.Characters
                                          select character;

            query = query.Where(x => characterNames.Contains(x.Name));

            return query.ToList();
        }

        public ICollection<Character> CreateOrRetrieveExisting(ICollection<string> characterNames)
        {
            ICollection<Character> characterList = GetCharactersByName(characterNames);

            foreach (string charName in characterNames)
            {
                if (!characterList.Any(x => x.Name == charName))
                {
                    Character character = new();
                    character.Name = charName;
                    dbContext.Characters.Add(character);
                    characterList.Add(character);
                }
            }
            dbContext.SaveChanges();
            return characterList;
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
