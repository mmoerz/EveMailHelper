using EveMailHelper.DataModels.Dto;

using EVEStandard.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IMarketManager
    {
        Task<SellBuyPriceDTO> ArchivedBuySellPrice(int regionId, int eveTypeId, int maxAgeInMinutes);
        Task<List<MarketOrder>> LoadMarketPrice(int regionId, int typeId, int page);
    }
}