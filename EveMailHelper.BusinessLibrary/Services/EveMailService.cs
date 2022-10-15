﻿using EveMailHelper.DataAccessLayer.Context;
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
    public class EveMailService : IEveMailService
    {
        #region injected
        private readonly IDbContextFactory<EveMailHelperContext> dbFactory = null!;
        #endregion
        private readonly EveMailHelperContext dbContext = null!;

        public EveMailService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            dbFactory = dbContextFactory;
            dbContext = dbFactory.CreateDbContext();
        }

        public async Task<EveMail> AddOrUpdate(EveMail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));

            dbContext.Update(eveMail);
            await dbContext.SaveChangesAsync();
            return eveMail;
        }

        public async Task<TableData<EveMail>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<EveMail> query = from mail in dbContext.EveMails
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

            return new TableData<EveMail>()
            {
                Items = await query
                .AsNoTracking()
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }
    }
}