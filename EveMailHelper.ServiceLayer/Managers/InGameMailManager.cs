using EveMailHelper.BusinessDataAccess;
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
        private readonly EveMailLabelDbAccess _eveMailLabelDbAccess;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EVEStandardAPI _esiClient;
        private readonly SSOv2 _sSOv2;

        public InGameMailManager(
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            EVEStandardAPI evestandard, SSOv2 ssov2
            )
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _eveMailLabelDbAccess = new(_dbContext);
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationManager = authenticationManager;
            _esiClient = evestandard;
            _sSOv2 = ssov2;
        }

        public async Task<TableData<EVEStandard.Models.Mail>> GetInboxMails(
            //AuthenticationState authenticationState,
            string searchString, TableState state)
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            var eveAccount = _authenticationManager.GetEveAccountFromPrincipal(user);
            var character = await _authenticationManager.GetCharacterFromPrincipal(user);

            AuthDTO auth = await _authenticationManager.GetAuthDTOForPrincipal(user);

            var labels = await SyncLabels(auth, character);

            var items = await _esiClient.Mail.ReturnMailHeadersV1Async(auth, new List<long>(), 0);                       

            //var items = await _esiClient.Mail.ReturnMailV1Async(auth, 0);
            //var chara = await _esiClient.Location.GetCharacterLocationV1Async(auth);

            return new TableData<EVEStandard.Models.Mail>()
            {
                Items = items.Model.ToList(),
                TotalItems = items.Model.Count()
            };
        }

        public async Task<IDictionary<int, EveMailLabel>> SyncLabels(AuthDTO auth, Character character)
        {
            var labelsAndCount = await _esiClient.Mail.GetMailLabelsAndUnreadCountsV3Async(auth);
            var labelDict = _eveMailLabelDbAccess.UpdateList(
                labelsAndCount.Model.Labels.toEveMailLabelList(character)
                );
            await _dbContext.SaveChangesAsync();
            return labelDict;
        }
    }
}
