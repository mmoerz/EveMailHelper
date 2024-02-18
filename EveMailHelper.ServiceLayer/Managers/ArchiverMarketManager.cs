using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels.Dto;
using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Utilities;

using EveNatTools.BusinessLogicLibrary;

using EVEStandard;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class ArchiverMarketManager : InGameMarketManager, IMarketManager
    {
        private readonly RunnerWriteDbAsync<List<MarketOrder>, List<MarketOrder>> _updateMarketOrdersForOneEveType;
        private readonly RunnerWriteDbAsync<List<MarketPrice>, List<MarketPrice>> _updateMarketPrices;
        private readonly MarketOrderDbAccess _marketorderDbAccess;
        private readonly MarketPriceDbAccess _marketpriceDbAccess;
        public ArchiverMarketManager(
            AuthenticationStateProvider authenticationStateProvider,
            IAuthenticationManager authenticationManager,
            IDbContextFactory<EveMailHelperContext> dbContextFactory,
            EVEStandardAPI esiClient, 
            SSOv2 ssov2
            )
            : base(authenticationStateProvider, authenticationManager, dbContextFactory, esiClient, ssov2 )
        {
            if (_dbContext == null)
                throw new Exception("aargs I died");
            _marketorderDbAccess = new MarketOrderDbAccess(_dbContext);
            _marketpriceDbAccess = new MarketPriceDbAccess(_dbContext);
            _updateMarketOrdersForOneEveType = new RunnerWriteDbAsync<List<MarketOrder>, List<MarketOrder>>
                (new UpdateMarketOrderAction(_marketorderDbAccess), _dbContext);
            _updateMarketPrices = new RunnerWriteDbAsync<List<MarketPrice>, List<MarketPrice>>
                (new UpdateMarketPriceAction(_marketpriceDbAccess), _dbContext );
        }

        public async Task<List<MarketOrder>> ArchivedMarketPrice(
            int regionId, int typeId, int page, int maxAgeInMinutes)
        {
            List<MarketOrder> marketOrders;
            var age = await _marketorderDbAccess.GetAgeForTypeIdAsync(typeId);

            if (age == 0 || age > (double)maxAgeInMinutes)
            {
                var esiList = await base.LoadMarketPrice(regionId, typeId, page);
                marketOrders = esiList.MapToMarketOrderList<EVEStandard.Models.MarketOrder>();
                marketOrders.ForEach(m => m.LastUpdatedFromEve = DateTime.UtcNow);
                marketOrders = await _updateMarketOrdersForOneEveType.RunAction(marketOrders);
            }
            else
                marketOrders = await _marketorderDbAccess.GetByTypeIdAsync(typeId);

            return marketOrders;
        }

        public async new Task<SellBuyPriceDTO> ArchivedBuySellPrice(
            int regionId, int eveTypeId, int maxAgeInMinutes)
        {
            List<MarketOrder> marketOrders;
            var age = await _marketorderDbAccess.GetAgeForTypeIdAsync(eveTypeId);

            if (age == 0 || age > (double)maxAgeInMinutes)
            {
                var esiList = await base.LoadMarketPrice(regionId, eveTypeId, 1);
                marketOrders = esiList.MapToMarketOrderList<EVEStandard.Models.MarketOrder>();
                marketOrders.ForEach(m => m.LastUpdatedFromEve = DateTime.UtcNow);
                marketOrders = await _updateMarketOrdersForOneEveType.RunAction(marketOrders);
            }
            return await _marketorderDbAccess.GetSellBuyForTypeIdAsync(eveTypeId);
        }

        public async new Task<MarketPrice> ArchivedMarketPrice(int eveTypeId, int maxAgeInMinutes)
        {
            List<MarketPrice> marketPrices;
            var age = await _marketpriceDbAccess.GetAgeForIdAsync(eveTypeId);

            if (age == 0 || age > (double)maxAgeInMinutes)
            {
                var esiList = await base.LoadPrices();
                marketPrices = esiList.MapToMarketPriceList<EVEStandard.Models.MarketPrice>();
                marketPrices.ForEach(m => m.LastUpdatedFromEve = DateTime.UtcNow);
                marketPrices = await _updateMarketPrices.RunAction(marketPrices);
            }
            return await _marketpriceDbAccess.GetByIdAsync(eveTypeId);
        }

    }
}
