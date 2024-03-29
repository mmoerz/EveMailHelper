﻿using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveNatTools.ServiceLibrary.Utilities;

namespace EveMailHelper.BusinessDataAccess
{
    public class NoteDbAccess
    {
        private readonly EveMailHelperContext _context;
        public NoteDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public void Delete(Note note)
        {
            _context.Notes.Remove(note);
        }

        public async Task<ICollection<Note>> GetAllAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(Guid id)
        {
            return await _context.Notes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TableData<Note>> GetPaginatedAsync(string searchString, TableState state)
        {
            IQueryable<Note> query = from mail in _context.Notes
                                                select mail;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                x.Title.Contains(searchString) ||
                x.Content.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Content" => query.OrderByDirection(state.SortDirection, x => x.Content),
                _ => query.OrderByDirection(state.SortDirection, x => x.Title),
            };
            var totalItems = query.Count();

            //if (state.Page > 0)
            //    query = query.Skip(state.Page * state.PageSize);
            //query = query.Take(state.PageSize);

            return new TableData<Note>()
            {
                Items = await query
                .Page(state.Page, state.PageSize)
                .AsNoTracking()
                .Include(note => note.AttachedTo)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public void Add(Note template)
        {
            _context.Notes.Add(template);
        }

        public void Update(Note template)
        {
            _context.Notes.Update(template);
        }
    }
}
