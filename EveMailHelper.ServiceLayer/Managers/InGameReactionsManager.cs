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

namespace EveMailHelper.ServiceLayer.Managers
{
    internal class InGameReactionsManager
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly EVEStandardAPI _esiClient;
        private readonly SSOv2 _sSOv2;

        public InGameReactionsManager(
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager,
            EVEStandardAPI esiClient,
            SSOv2 sSOv2)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationManager = authenticationManager;
            _esiClient = esiClient;
            _sSOv2 = sSOv2;
        }

        public async Task<List<String>> LoadReactions()
        {
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            AuthDTO auth = await _authenticationManager.GetAuthDTOForPrincipal(user);

            // Market
            //_esiClient.Universe
            return new List<String>();
        }
    }
}
