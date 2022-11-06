using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dbAccess
{
    public class ChatDbAccess
    {
        private readonly EveMailHelperContext _context;
        public ChatDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Chat>> GetAll()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat> GetById(Guid id)
        {
            var result = await _context.Chats.Where(x => x.Id == id).FirstOrDefaultAsync();
            _ = result ?? throw new NullReferenceException($"guid {id} not a valid ChatId");
            return result;
        }

        public async Task<TableData<Chat>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<Chat> query = from ch in _context.Chats
                                                select ch;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => 
                x.ChannelName.Contains(searchString) ||
                x.Listener.Name.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "Listener" => query.OrderByDirection(state.SortDirection, x => x.Listener.Name),
                _ => query.OrderByDirection(state.SortDirection, x => x.ChannelName),
            };
            var totalItems = query.Count();

            if (state.Page > 0)
                query = query.Skip(state.Page * state.PageSize);
            query = query.Take(state.PageSize);

            return new TableData<Chat>()
            {
                Items = await query
                .AsNoTracking()
                .Include(note => note.AttachedTo)
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public void Add(Chat chat)
        {
            _context.Chats.Add(chat);
        }

        public void Update(Chat chat)
        {
            _context.Chats.Update(chat);
        }
    }
}
