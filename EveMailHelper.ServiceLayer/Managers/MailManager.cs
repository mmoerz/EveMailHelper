using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessLibrary.Utilities;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using EveNatTools.ServiceLibrary.Utilities;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class MailManager : IMailManager
    {
        #region injected
        #endregion
        private readonly EveMailHelperContext _dbContext = null!;
        private readonly CharacterDbAccess _characterDbAccess;
        private readonly CorporationDbAccess _corporationDbAccess;
        private readonly AllianceDbAccess _allianceDbAccess;
        private readonly MailListDbAccess _mailListDbAccess;
        private readonly EveMailTemplateDbAccess _templateDbAccess;
        private readonly MailDbAccess _evemailDbAccess;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly RunnerWriteDb<ICollection<string>, ICollection<Character>> _addCharRunner;
        private readonly RunnerWriteDbAsync<SendTemplateToDto, Mail> _sendTemplateToRunner;
        private readonly RunnerWriteDb<Mail, Mail> _updateMailRunner;

        public MailManager(IDbContextFactory<EveMailHelperContext> dbContextFactory,
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager)
        {
            var factory = dbContextFactory;
            _dbContext = factory.CreateDbContext();
            _characterDbAccess = new CharacterDbAccess(_dbContext);
            _corporationDbAccess = new CorporationDbAccess(_dbContext);
            _allianceDbAccess = new AllianceDbAccess(_dbContext);
            _mailListDbAccess = new MailListDbAccess(_dbContext);
            _templateDbAccess = new EveMailTemplateDbAccess(_dbContext);
            _evemailDbAccess = new MailDbAccess(_dbContext);
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationManager = authenticationManager;
            _addCharRunner = new RunnerWriteDb<ICollection<string>, ICollection<Character>>
                (new AddCharactersByNameAction(_characterDbAccess), _dbContext);
            _sendTemplateToRunner = new RunnerWriteDbAsync<SendTemplateToDto, Mail>
                (new SendEveMailBasedOnTemplateAction(_evemailDbAccess), _dbContext);
            _updateMailRunner = new RunnerWriteDb<Mail, Mail>
                (new UpdateEveMailAction(_evemailDbAccess), _dbContext);
        }

        public void Delete(Mail eveMail)
        {
            _ = eveMail ?? throw new ArgumentNullException(nameof(eveMail));
            if (eveMail.Id == Guid.Empty)
                throw new ArgumentException("null Guid is invalid", nameof(eveMail));
            // now load the Evemail with all Sendto entities (child's that depend on it)
            IQueryable<Mail> query = from mail in _dbContext.Mails
                                        select mail;
            query = query.Where(mail => mail.Id == eveMail.Id);
            var result = query.Include(mail => mail.SentTo).First();

            _dbContext.Mails.Remove(result);
            _dbContext.SaveChanges();
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
            IQueryable<Mail> query = from mail in _dbContext.Mails
                                        select mail;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.Subject.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "From" => query.OrderByDirection(state.SortDirection, x => x.From.Name),
                "Content" => query.OrderByDirection(state.SortDirection, x => x.Content),
                "Labels" => query.OrderByDirection(state.SortDirection, x => x.Labels),
                "Subject" => query.OrderByDirection(state.SortDirection, x => x.Subject),
                "IsRead" => query.OrderByDirection(state.SortDirection, x => x.IsRead),
                _ => query.OrderByDirection(state.SortDirection, x => x.CreatedDate),
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

        public async Task<TableData<Mail>> GetPaginatedCurrentCharacter(
            string searchString, TableState state
            )
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var eveAccount = _authenticationManager.GetEveAccountFromPrincipal(user);
            var character = await _authenticationManager.GetCharacterFromPrincipal(user);
            return await GetPaginated(character, searchString, state);
        }

        public async Task<TableData<Mail>> GetPaginated(Character fromCharacter,
            string searchString, TableState state)
        {
            var query = from mail in _dbContext.Mails
                        where mail.Owner == fromCharacter
                        select mail;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.Subject.Contains(searchString) ||
                    m.Content.Contains(searchString));
            }

            query = state.SortLabel switch
            {
                "From" => query.OrderByDirection(state.SortDirection, x => x.From.Name),
                "Content" => query.OrderByDirection(state.SortDirection, x => x.Content),
                "Labels" => query.OrderByDirection(state.SortDirection, x => x.Labels),
                "Subject" => query.OrderByDirection(state.SortDirection, x => x.Subject),
                "IsRead" => query.OrderByDirection(state.SortDirection, x => x.IsRead),
                _ => query.OrderByDirection(state.SortDirection, x => x.CreatedDate),
            };

            var itemCount = query.Count();

            return new TableData<Mail>()
            {
                Items = await query
                    .Page(state.Page, state.PageSize)
                    .Include(x => x.From)
                    .Include(x => x.Labels)
                    .Include(x => x.Recipients)
                    .ToListAsync(),
                TotalItems = itemCount
            };
        }

        public async Task SendTo(Guid templateId, Character fromCharacter, ICollection<string> receiverNames)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
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

        public async Task<Mail> GetReceivers(Mail mail)
        {
            var query = _dbContext.Mails
                .Where(x => x.Id == mail.Id)
                .Include(x => x.From)
                .Include(x => x.Labels)
                .Include(x => x.Recipients);
            var result = await query.FirstAsync();

            foreach (var recipient in result.Recipients)
            {
                if (recipient is MailRecipientCharacter)
                {
                    var help = (MailRecipientCharacter) recipient;
                    help.Character = _characterDbAccess.GetById(help.CharacterId);
                }
                if (recipient is MailRecipientCorporation)
                {
                    var help = (MailRecipientCorporation)recipient;
                    help.Corporation = _corporationDbAccess.GetById(help.CorporationId);
                }
                if (recipient is MailRecipientAlliance)
                {
                    var help = (MailRecipientAlliance)recipient;
                    help.Alliance = _allianceDbAccess.GetById(help.AllianceId);
                }
                if (recipient is MailRecipientMailList)
                {
                    var help = (MailRecipientMailList)recipient;
                    help.MailList = _mailListDbAccess.GetById(help.MailListId);
                }
            }

            return result;
        }
    }
}
