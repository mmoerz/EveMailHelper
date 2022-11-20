using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.ServiceLibrary.Managers;

using EVEStandard;
using EVEStandard.Models.API;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class InGameMailManager : IInGameMailManager
    {
        private readonly EveMailHelperContext _dbContext;
        //private readonly EveMailLabelDbAccess _eveMailLabelDbAccess;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EVEStandardAPI _esiClient;
        private readonly SSOv2 _sSOv2;

        private readonly RunnerWriteDb<AddLabelDTO, IDictionary<long, MailLabel>> updateEveMailLabels;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Character>> addEsCharacters;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Corporation>> addEsCorporations;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Alliance>> addEsAlliances;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, MailList>> addEsMailList;
        private readonly RunnerWriteDbAsync<AddMailDTO, ICollection<Mail>> addEsMails;

        // TODO: remove this
        private readonly MailDbAccess _mailDbAccess;
        

        public InGameMailManager(
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            EVEStandardAPI esiClient, SSOv2 ssov2
            )
        {
            _dbContext = dbContextFactory.CreateDbContext();
            var eveMailLabelDbAccess = new MailLabelDbAccess(_dbContext);
            var characterDbAccess = new CharacterDbAccess(_dbContext);
            var corporationDbAccess = new CorporationDbAccess(_dbContext);
            var allianceDbAccess = new AllianceDbAccess(_dbContext);
            var maillistDbAccess = new MailListDbAccess(_dbContext);
            var eveMailDbAccess = new MailDbAccess(_dbContext);
            _mailDbAccess = eveMailDbAccess;

            _authenticationStateProvider = authenticationStateProvider;
            _authenticationManager = authenticationManager;
            _esiClient = esiClient;
            _sSOv2 = ssov2;

            updateEveMailLabels = new(
                new UpdateEveMailLabelsAction(eveMailLabelDbAccess, characterDbAccess),
                _dbContext
                );
            addEsCharacters = new(
                new AddEsCharactersAction(characterDbAccess, _esiClient),
                _dbContext
                );
            addEsCorporations = new(
                new AddEsCorporationsAction(corporationDbAccess, _esiClient),
                _dbContext
                );
            addEsAlliances = new(
                new AddEsAlliancesAction(allianceDbAccess, _esiClient),
                _dbContext
                );
            addEsMailList = new(
                new AddEsMailListsAction(maillistDbAccess, _esiClient),
                _dbContext
                );
            addEsMails = new(
                new AddEveMailsAction(eveMailDbAccess, characterDbAccess, _esiClient),
                _dbContext
                );
        }

        public async Task<TableData<Mail>> GetInboxMails(
            string searchString, TableState state)
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var eveAccount = _authenticationManager.GetEveAccountFromPrincipal(user);
            var character = await _authenticationManager.GetCharacterFromPrincipal(user);

            AuthDTO auth = await _authenticationManager.GetAuthDTOForPrincipal(user);

            var labelsAndCount = await _esiClient.Mail.GetMailLabelsAndUnreadCountsV3Async(auth);

            var eveLabels = updateEveMailLabels.RunAction(new AddLabelDTO()
            {
                CharacterId = character.Id,
                MailLabels = labelsAndCount.Model.Labels
            });

            var lastEveMailId = await _mailDbAccess.GetMaxEveMailIdAsync(character.Id);
            // this doesn't seem to work properly ... lastmailid is properly given, but doesn't seem to influence the
            // mails that are transfered
            var headers = await _esiClient.Mail.ReturnMailHeadersV1Async(auth, new List<long>(), lastEveMailId);


            var idLists = ExtractIdsFromMails(headers.Model);
            var mailDTO = new AddMailDTO()
            {
                authDTO= auth,
                Labels = eveLabels,
                Characters = await addEsCharacters.RunAction(idLists.EsCharacterIds),
                Corporations = await addEsCorporations.RunAction(idLists.EsCorporationIds),
                Alliances = await addEsAlliances.RunAction(idLists.EsAlliances),
                MailLists = await addEsMailList.RunAction(idLists.EsMailingLists),
                esMailHeaders = headers.Model
            };
            // now add all recipients
            var mails = await addEsMails.RunAction(mailDTO);

            // download the content

            //var items = await _esiClient.Mail.ReturnMailV1Async(auth, 0);
            //var chara = await _esiClient.Location.GetCharacterLocationV1Async(auth);

            return new TableData<Mail>()
            {
                Items = mails.ToList(),
                TotalItems = mails.Count()
            };
        }

        private class EsIdLists
        {
            public EsIdLists()
            {
                EsCharacterIds = new HashSet<int>();
                EsCorporationIds = new HashSet<int>();
                EsAlliances = new HashSet<int>();
                EsMailingLists = new HashSet<int>();
            }

            public ISet<int> EsCharacterIds;
            public ISet<int> EsCorporationIds;
            public ISet<int> EsAlliances;
            public ISet<int> EsMailingLists;
        }

        private EsIdLists ExtractIdsFromMails(ICollection<EVEStandard.Models.Mail> esMails)
        {
            EsIdLists result = new();

            foreach (var mail in esMails)
            {
                if (mail.From != null)
                    result.EsCharacterIds.Add(mail.From.Value);
                foreach (var recipient in mail.Recipients)
                {
                    if (recipient == null) continue;
                    switch (recipient.RecipientType)
                    {
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.character:
                            result.EsCharacterIds.Add(recipient.RecipientId);
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.alliance:
                            result.EsAlliances.Add(recipient.RecipientId);
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.corporation:
                            result.EsCorporationIds.Add(recipient.RecipientId);
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.mailing_list:
                            result.EsMailingLists.Add(recipient.RecipientId);
                            break;
                    }
                }
            }

            return result;
        }

    }
}
