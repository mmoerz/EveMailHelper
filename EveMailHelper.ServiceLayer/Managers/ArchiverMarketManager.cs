using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels.Dto;
using EveMailHelper.DataModels.Market;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Utilities;

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
        private readonly RunnerWriteDb<List<MarketOrder>, List<MarketOrder>> _updateMarketOrdersForOneEveType;
        private readonly MarketOrderDbAccess _marketorderDbAccess;

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
            _updateMarketOrdersForOneEveType = new RunnerWriteDb<List<MarketOrder>, List<MarketOrder>>
                (new UpdateMarketOrderAction(_marketorderDbAccess), _dbContext);
        }

        public async Task<List<MarketOrder>> ArchivedMarketPrice(
            int regionId, int typeId, int page, int maxAgeInMinutes)
        {
            List<MarketOrder> marketOrders;
            var age = await _marketorderDbAccess.GetAgeForTypeIdAsync(typeId);

            if (age == null || age > (double)maxAgeInMinutes)
            {
                var esiList = await base.LoadMarketPrice(regionId, typeId, page);
                marketOrders = esiList.MapToMarketOrderList<EVEStandard.Models.MarketOrder>();
                marketOrders.ForEach(m => m.lastUpdated = DateTime.UtcNow);
                marketOrders = _updateMarketOrdersForOneEveType.RunAction(marketOrders);
                _dbContext.SaveChanges();
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
                marketOrders.ForEach(m => m.lastUpdated = DateTime.UtcNow);
                marketOrders = _updateMarketOrdersForOneEveType.RunAction(marketOrders);
                //_dbContext.SaveChanges();
            }
            return await _marketorderDbAccess.GetSellBuyForTypeIdAsync(eveTypeId);
        }

        

    }
}
