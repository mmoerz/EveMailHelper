using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class CharacterDbAccess
    {
        private readonly EveMailHelperContext _context;
        public CharacterDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Character Add(Character character)
        {
            var characterFixed = CheckAndFix(character);
            _context.Characters.Add(characterFixed);
            return characterFixed;
        }

        public Character CheckAndFix(Character character)
        {
            if (character.Account == null || character.Account.Id == Guid.Empty)
            {
                character.Account = new() { NickName = character.Name };
            }
            if (character.EveAccount == null || character.EveAccount.Id == Guid.Empty)
            {
                character.EveAccount = new() { Name = character.Name };
                character.Account.EveAccounts.Add(character.EveAccount);
            }
            return character;
        }

        public Character GetById(Guid id)
        {
            return _context.Characters.Where(character => character.Id == id).First();
        }

        public ICollection<Character> GetByNames(ICollection<string> characterNames)
        {
            IQueryable<Character> query = from character in _context.Characters
                                          select character;

            query = query.Where(x => characterNames.Contains(x.Name));

            return query.ToList();
        }

        public async Task<TableData<Character>> GetCharactersPaginated(Account account, EveAccount? eveaccount, string searchString, TableState state)
        {
            IQueryable<Character> query = from ch in _context.Characters
                                          where ch.AccountId == account.Id
                                          select ch;
            if (eveaccount != null)
            {
                query.Where(x => x.EveAccountId == eveaccount.Id);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x =>
                    x.Name.Contains(searchString)
                    || x.Description != null && x.Description.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Description" => query.OrderByDirection(state.SortDirection, x => x.Description),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<Character>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .Include(x => x.Account)
                    .Include(x => x.EveAccount)
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }

        public void Update(Character character)
        {
            _context.Characters.Update(character);
        }
    }
}
