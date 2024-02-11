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
    public class InGameMarketManager : IMarketManager
    {
        protected readonly EveMailHelperContext _dbContext;
        protected readonly AuthenticationStateProvider _authenticationStateProvider;
        protected readonly IAuthenticationManager _authenticationManager;
        protected readonly EVEStandardAPI _esiClient;
        protected readonly SSOv2 _sSOv2;

        protected readonly MapDbAccess _mapDbAccess;

        public InGameMarketManager(
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            EVEStandardAPI esiClient, SSOv2 ssov2
            )
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _mapDbAccess = new MapDbAccess(_dbContext);

            _authenticationStateProvider = authenticationStateProvider;
            _authenticationManager = authenticationManager;
            _esiClient = esiClient;
            _sSOv2 = ssov2;
        }

        public async Task<List<EVEStandard.Models.MarketOrder>> LoadMarketPrice(int regionId, int typeId, int page)
        {
            //var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            //AuthDTO auth = await _authenticationManager.GetAuthDTOForPrincipal(user);

            var esiresult = await _esiClient.Market.ListOrdersInRegionV1Async(regionId, typeId, 1);
            _ = esiresult ?? throw new Exception("LoadMarketPrice esi result null");

            if (esiresult.RemainingErrors != 0)
                throw new Exception("errors on esicall");

            return esiresult.Model;
        }


    }
}
