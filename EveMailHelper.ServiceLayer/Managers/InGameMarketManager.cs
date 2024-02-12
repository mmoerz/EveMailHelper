using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels.Dto;
using EveMailHelper.DataModels.Market;
using EveMailHelper.ServiceLayer.Interfaces;

using EVEStandard;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

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

        public Task<SellBuyPriceDTO> ArchivedBuySellPrice(int regionId, int eveTypeId, int maxAgeInMinutes)
        {
            throw new NotImplementedException();
        }

        public Task<MarketPrice> GetMarketPrice(int eveTypeId, int maxAgeInMinutes)
        {
            throw new NotImplementedException();
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

        public async Task<List<EVEStandard.Models.MarketPrice>> LoadPrices()
        {
            var esiresult = await _esiClient.Market.ListMarketPricesV1Async();
            _ = esiresult ?? throw new Exception("LoadPrices esi result is null");

            if (esiresult.RemainingErrors != 0)
                throw new Exception("errors on esicall");

            return esiresult.Model;
        }
        
    }
}
