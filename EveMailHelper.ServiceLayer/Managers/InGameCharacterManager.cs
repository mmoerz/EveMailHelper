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
    public class InGameCharacterManager 
    {
        private readonly EveMailHelperContext _dbContext;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EVEStandardAPI _esiClient;
        private readonly SSOv2 _sSOv2;

        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Character>> addEsCharacters;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Corporation>> addEsCorporations;
        private readonly RunnerWriteDbAsync<ICollection<int>, IDictionary<int, Alliance>> addEsAlliances;

        // TODO: remove this
        private readonly MailDbAccess _mailDbAccess;


        public InGameCharacterManager(
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

            addEsCharacters = new(
                new AddEsCharactersAction(characterDbAccess, corporationDbAccess, allianceDbAccess, _esiClient),
                _dbContext
                );
            addEsCorporations = new(
                new AddEsCorporationsAction(corporationDbAccess, _esiClient),
                _dbContext
                );
            addEsAlliances = new(
                new AddEsAlliancesAction(allianceDbAccess, corporationDbAccess, _esiClient),
                _dbContext
                );
        }

        public async Task LoadCharactersByName(List<string> CharacterNames)
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var eveAccount = _authenticationManager.GetEveAccountFromPrincipal(user);
            var character = await _authenticationManager.GetCharacterFromPrincipal(user);

            AuthDTO auth = await _authenticationManager.GetAuthDTOForPrincipal(user);

            foreach(var charName in CharacterNames)
            {
                var found = await _esiClient.Search.SearchCharacterV3Async(
                    auth, new List<string>() { "character" }, charName, true);
                if (found == null) continue;
                var foundCharacterIds = found.Model.Character;
                var Characters = await addEsCharacters.RunAction(foundCharacterIds);

               

            }

            //Corporations = await addEsCorporations.RunAction(idLists.EsCorporationIds);
            //Alliances = await addEsAlliances.RunAction(idLists.EsAlliances);

        }

    }
}
