using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class AccountDbAccess
    {
        private readonly EveMailHelperContext _context;
        public AccountDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Account GetCharactersById(Guid id)
        {
            return _context.Accounts.Where(account => account.Id == id)
                .AsTracking()
                .Include(x => x.EveAccounts)
                // ?? eveaccount->characters?
                .Include(x => x.Characters)
                .First();
        }

        public async Task<TableData<EveAccount>> GetEveAccountsPaginated(Account account, string searchString, TableState state)
        {
            IQueryable<EveAccount> query = from eveaccount in _context.EveAccounts
                                          select eveaccount;
            
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                    x.Name.Contains(searchString)
                    || x.Description.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Description" => query.OrderByDirection(state.SortDirection, x => x.Description),
                _ => query.OrderByDirection(state.SortDirection, x => x.Name),
            };
            var totalItems = query.Count();

            return new TableData<EveAccount>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .AsTracking()
                    .Include(x => x.Account)
                    .Include(x => x.Characters)
                    .ToListAsync(),
                TotalItems = totalItems
            };
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

        public void Add(Character character)
        {
            if (character.Account == null)
            {
                character.Account = new() { NickName = character.Name };
            }
            if (character.EveAccount == null)
            {
                character.EveAccount = new() { Name = character.Name };
                character.Account.EveAccounts.Add(character.EveAccount);
            }
            _context.Characters.Add(character);
        }

        public void Update(Character character)
        {
            _context.Characters.Update(character);
        }
    }
}
