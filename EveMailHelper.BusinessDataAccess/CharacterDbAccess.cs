﻿using EveMailHelper.BusinessDataAccess.Interfaces;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class CharacterDbAccess : IEveId<Character>, IUpdateModel<Character>
    {
        const int MaxCacheMinutesDefault = 24 * 60;

        private readonly EveMailHelperContext _context;
        public CharacterDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Character Add(Character character)
        {
            var characterFixed = CheckAndFix(character);
            character.EveLastUpdated= DateTime.UtcNow;
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

        public async Task<Character> GetByIdAsync(Guid id)
        {
            return await _context.Characters.Where(character => character.Id == id).FirstAsync();
        }

        public Character GetById(Guid id)
        {
            return _context.Characters.Where(character => character.Id == id).First();
        }

        public async Task<List<Character>> GetByIdsFullTrackedAsync(List<Guid> ids)
        {
            return await _context.Characters
                .Where(character => ids.Contains(character.Id))
                .AsTracking()
                .Include(x => x.Corporation)
                .ToListAsync();
        }

        public Character GetByEveId(int? id)
        {
            return _context.Characters
                .Where(character => character.EveId == id)
                .First();
        }

        public ICollection<Character> GetByNames(ICollection<string> characterNames)
        {
            IQueryable<Character> query = from character in _context.Characters
                                          select character;

            query = query.Where(x => characterNames.Contains(x.Name));

            return query.ToList();
        }

        public ICollection<Character> GetByEveIds(ICollection<int> characterEveIds)
            
        {
            var query = _context.Characters
                .Where(character => characterEveIds.Contains(character.EveId));

            return query.ToList();
        }

        public async Task<TableData<Character>> GetCharactersPaginatedAsync(Account account, EveAccount? eveaccount, string searchString, TableState state)
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

        public Character Update(Character character)
        {
            character.EveLastUpdated = DateTime.UtcNow;
            _context.Characters.Update(character);
            return character;
        }
    }
}
