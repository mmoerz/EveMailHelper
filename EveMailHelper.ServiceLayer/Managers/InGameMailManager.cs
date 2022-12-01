using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Utilities;
using EveMailHelper.ServiceLibrary.Managers;

using EveNatTools.ServiceLibrary.Utilities;

using EVEStandard;
using EVEStandard.Models.API;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly RunnerWriteDbAsync<CharCorpAllianceDTO, CharCorpAllianceDTO> addEsCharacters;
        private readonly RunnerWriteDb<CharCorpAllianceDTO, CharCorpAllianceDTO> addExtEsCharacterAction;
        private readonly RunnerWriteDb<CharCorpAllianceDTO, CharCorpAllianceDTO> addExtEsAllianceAction;
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
                new AddEsCharactersAction(characterDbAccess, corporationDbAccess, allianceDbAccess, _esiClient),
                _dbContext
                );
            addExtEsCharacterAction = new (
                new AddExtEsCharacterAction(characterDbAccess, corporationDbAccess, allianceDbAccess, _esiClient),
                _dbContext
                );
            addExtEsAllianceAction = new(
                new AddExtEsAllianceAction(characterDbAccess, corporationDbAccess, allianceDbAccess, _esiClient),
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

        public async Task GetInboxMails()
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
            //var headers2 = await _esiClient.Mail.ReturnMailHeadersV1Async(auth, new List<long>(), lastEveMailId - 400);
            long nextEveMailId = lastEveMailId;
            nextEveMailId = int.MaxValue;

            CharCorpAllianceDTO ccaDTO = new();

            while (nextEveMailId > 0)
            {
                var headers = await _esiClient.Mail.ReturnMailHeadersV1Async(auth, new List<long>(), nextEveMailId);
                if (headers.Model.Count == 50) // yeah maximum, maybe there are more mails??
                    nextEveMailId = (headers.Model.Last().MailId ?? 0) - 1;
                else
                    nextEveMailId = 0;

                var idLists = await ExtractIdsFromMails(headers.Model);
                
                ccaDTO.CharactersDD.UnionWith(idLists.EsCharacterIds);
                ccaDTO.CorporationsDD.UnionWith(idLists.EsCorporationIds);
                ccaDTO.AlliancesDD.UnionWith(idLists.EsAlliances);

                var resultChar1 = await addEsCharacters.RunAction(ccaDTO);
                // now fixup alliances and save that
                var resultChar2 = addExtEsAllianceAction.RunAction(resultChar1);
                // now fixup the characters and corps and save them again
                var resultChar3 = addExtEsCharacterAction.RunAction(resultChar2);

                var mailDTO = new AddMailDTO()
                {
                    authDTO = auth,
                    Labels = eveLabels,
                    Characters = resultChar3.CharactersDD.Models,
                    Corporations = resultChar3.CorporationsDD.Models,
                    Alliances = resultChar3.AlliancesDD.Models,
                    MailLists = await addEsMailList.RunAction(idLists.EsMailingLists),
                    esMailHeaders = headers.Model,
                    Owner = character
                };
                // now add all recipients
                var mails = await addEsMails.RunAction(mailDTO);

            }
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

        private async Task<EsIdLists> ExtractIdsFromMails(ICollection<EVEStandard.Models.Mail> esMails)
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
                            var dto = await _esiClient.Alliance.GetAllianceInfoV3Async(recipient.RecipientId);
                            result.EsCharacterIds.Add(dto.Model.CreatorId);
                            result.EsCorporationIds.Add(dto.Model.CreatorCorporationId);
                            if (dto.Model.ExecutorCorporationId != null)
                                result.EsCorporationIds.Add(dto.Model.ExecutorCorporationId.Value);
                            result.EsAlliances.Add(recipient.RecipientId);
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.corporation:
                            var corp = await _esiClient.Corporation.GetCorporationInfoV5Async(recipient.RecipientId);
                            result.EsCorporationIds.Add(recipient.RecipientId);
                            result.EsCharacterIds.Add(corp.Model.CeoId);
                            result.EsCharacterIds.Add(corp.Model.CreatorId);
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
