using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessLibrary.Utilities;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class MailManager : IMailService
    {
        #region injected
        #endregion
        private readonly EveMailHelperContext _context = null!;
        private readonly CharacterDbAccess _characterDbAccess;
        private readonly EveMailTemplateDbAccess _templateDbAccess;
        private readonly MailDbAccess _evemailDbAccess;
        private readonly RunnerWriteDb<ICollection<string>, ICollection<Character>> _addCharRunner;
        private readonly RunnerWriteDbAsync<SendTemplateToDto, Mail> _sendTemplateToRunner;
        private readonly RunnerWriteDb<Mail, Mail> _updateMailRunner;

        public MailManager(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            var factory = dbContextFactory;
            _context = factory.CreateDbContext();
            _characterDbAccess = new CharacterDbAccess(_context);
            _templateDbAccess = new EveMailTemplateDbAccess(_context);
            _evemailDbAccess = new MailDbAccess(_context);
            _addCharRunner = new RunnerWriteDb<ICollection<string>, ICollection<Character>>
                (new AddCharactersByNameAction(_characterDbAccess), _context);
            _sendTemplateToRunner = new RunnerWriteDbAsync<SendTemplateToDto, Mail>
                (new SendEveMailBasedOnTemplateAction(_evemailDbAccess), _context);
            _updateMailRunner = new RunnerWriteDb<Mail, Mail>
                (new UpdateEveMailAction(_evemailDbAccess), _context);
        }

        public void Delete(Mail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            if (eveMail.Id == Guid.Empty)
                throw new ArgumentException("null Guid is invalid", nameof(eveMail));
            // now load the Evemail with all Sendto entities (child's that depend on it)
            IQueryable<Mail> query = from mail in _context.EveMails
                                        select mail;
            query = query.Where(mail => mail.Id == eveMail.Id);
            var result = query.Include(mail => mail.SentTo).First();

            _context.EveMails.Remove(result);
            _context.SaveChanges();
        }

        public Mail Update(Mail eveMail)
        {
            return _updateMailRunner.RunAction(eveMail);
        }

        public async Task<List<string>> FilterReceivers(string receivers, DateTime filterTime)
        {
            var result = await _evemailDbAccess.GetReceiversFiltered(receivers.SplitStringOfCharacters(','), filterTime);
            //List<string> receiversFiltered = new List<string>();
            //foreach (var item in result)
            //    receiversFiltered.Add(item.Name);
            //receiversFiltered.AddRange(result);

            //return receiversFiltered.OrderBy(x => x).ToList();
            //return receiversFiltered;
            return result;
        }

        public async Task<TableData<Mail>> GetPaginated(string searchString, TableState state)
        {
            IQueryable<Mail> query = from mail in _context.EveMails
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

            return new TableData<Mail>()
            {
                Items = await query
                .AsNoTracking()
                .ToListAsync(),
                TotalItems = totalItems,
            };
        }

        public async Task SendTo(Guid templateId, Character fromCharacter, ICollection<string> receiverNames)
        {
            using var transaction = _context.Database.BeginTransaction();
            var template = await _templateDbAccess.GetById(templateId);
            var character = await _characterDbAccess.GetByIdAsync(fromCharacter.Id);

            if (template == null)
                throw new Exception($"cannot find template with id {templateId}");

            var characters = _addCharRunner.RunAction(receiverNames);
            SendTemplateToDto dto = new()
            {
                Template = template,
                FromCharacter = character,
                Characters = characters
            };

            var mail = await _sendTemplateToRunner.RunAction(dto);

            await transaction.CommitAsync();
        }

        
    }
}
