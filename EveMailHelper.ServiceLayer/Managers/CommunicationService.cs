using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.DataAccessLayer.Context;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Tools;
using EveMailHelper.ServiceLayer.Interfaces;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using EveMailHelper.BusinessLibrary.Complex.dto;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class CommunicationService : ICommunicationService
    {
        #region injected
        private readonly EveMailHelperContext _context = null!;
        //private readonly CharacterDbAccess _characterDbAccess;
        private readonly EveMailDbAccess _evemailDbAccess;
        #endregion

        public CommunicationService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            var factory = dbContextFactory;
            _context = factory.CreateDbContext();
            //_characterDbAccess = new CharacterDbAccess(_context);
            _evemailDbAccess = new EveMailDbAccess(_context);
        }

        public async Task<TableData<EveMail>> GetPaginatedEveMail(Character character, string searchString, TableState state)
        {
            IQueryable<EveMail> query = from mail in _context.EveMails
                                        select mail;
            IQueryable<Guid> querySendTos =
                from sendTo in _context.EveMailSentTos
                where sendTo.CharacterId == character.Id
                select sendTo.EveMailId;

            int searchInt = searchString.SafeParseInt();

            // join (reduce to) the mails of the character 
            query = query.Where(mail => querySendTos.Contains(mail.Id));

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x =>
                x.Subject.Contains(searchString) ||
                (searchInt > 0 && (x.CreatedDate.Day == searchInt ||
                                   x.CreatedDate.Month == searchInt ||
                                   x.CreatedDate.Year == searchInt))
                );
            }

            query = state.SortLabel switch
            {
                "CreateDate" => query.OrderByDirection(state.SortDirection, x => x.CreatedDate),
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

        public async Task<TableData<Chat>> GetPaginatedChats(Character character, string searchString, TableState state)
        {
            IQueryable<Chat> query = from mail in _context.Chats
                                     select mail;

            //IQueryable<Guid> queryChars =
            //    from ch in _context.Characters
            //    where ch.Name.Contains(searchString)
            //    select ch.Id;

            IQueryable<Guid> queryAuthors =
                from msg in _context.ChatMessages
                where msg.AuthorId == character.Id
                select msg.ChatId;

            //IQueryable < Guid > queryChatMsgs =
            //    from chatmsg in _context.ChatMessages
            //    where chatmsg.Message.Contains(searchString)
            //    select chatmsg.ChatId;

            int searchInt = searchString.SafeParseInt();

            // join (reduce to) the mails of the character 
            query = query.Where(chat => queryAuthors.Contains(chat.Id));

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x =>
                x.ChannelName.Contains(searchString) ||
                x.Listener.Name.Contains(searchString) ||
                (searchInt > 0 && (x.SessionStarted.Day == searchInt ||
                                   x.SessionStarted.Month == searchInt ||
                                   x.SessionStarted.Year == searchInt))
                );
            }

            query = state.SortLabel switch
            {
                "Listener" => query.OrderByDirection(state.SortDirection, x => x.Listener.Name),
                "SessionStarted" => query.OrderByDirection(state.SortDirection, x => x.SessionStarted),
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
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public async Task<TableData<Note>> GetPaginatedNotes(Character character, string searchString, TableState state)
        {
            IQueryable<Note> query = from note in _context.Notes
                                     where note.AttachedToId == character.Id
                                     select note;

            int searchInt = searchString.SafeParseInt();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(note =>
                note.Title.Contains(searchString) ||
                note.Title.Contains(searchString) ||
                (searchInt > 0 && (note.CreatedDate.Day == searchInt ||
                                   note.CreatedDate.Month == searchInt ||
                                   note.CreatedDate.Year == searchInt))
                );
            }

            query = state.SortLabel switch
            {
                "CreateDate" => query.OrderByDirection(state.SortDirection, x => x.CreatedDate),
                _ => query.OrderByDirection(state.SortDirection, x => x.Title),
            };
            var totalItems = query.Count();

            if (state.Page > 0)
                query = query.Skip(state.Page * state.PageSize);
            query = query.Take(state.PageSize);

            return new TableData<Note>()
            {
                Items = await query
                .AsNoTracking()
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public async Task<TableData<Communication>> GetPaginated(Character character, string searchString, TableState state)
        {
            if (character == null)
                return new TableData<Communication>() { Items = new List<Communication>(), TotalItems = 0 };

            var eveMails = await GetPaginatedEveMail(character, searchString, state);

            List<Communication> communicationList = new();
            foreach (var eveMail in eveMails.Items)
            {
                communicationList.Add(new Communication()
                {
                    Id = eveMail.Id,
                    CreatedDate = eveMail.CreatedDate,
                    obj = eveMail,
                    Name = eveMail.Subject,
                });
            }

            var eveChats = await GetPaginatedChats(character, searchString, state);
            foreach (var eveChat in eveChats.Items)
            {
                communicationList.Add(new Communication()
                {
                    Id = eveChat.Id,
                    CreatedDate = eveChat.SessionStarted,
                    obj = eveChat,
                    Name = eveChat.ChannelName,
                });
            }

            var Notes = await GetPaginatedNotes(character, searchString, state);
            foreach (var note in Notes.Items)
            {
                communicationList.Add(new Communication()
                {
                    Id = note.Id,
                    CreatedDate = note.CreatedDate,
                    obj = note,
                    Name = note.Title,
                });
            }

            var result = state.SortLabel switch
            {
                "CreateDate" => communicationList.OrderByDirection(state.SortDirection, x => x.CreatedDate),
                _ => communicationList.OrderByDirection(state.SortDirection, x => x.Name),
            };

            return new TableData<Communication>()
            {
                Items = communicationList,
                TotalItems = eveMails.TotalItems + eveChats.TotalItems + Notes.TotalItems,
            };
        }
    }
}
