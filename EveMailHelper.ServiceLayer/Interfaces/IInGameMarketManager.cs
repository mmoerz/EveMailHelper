using EVEStandard.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IInGameMarketManager
    {
        Task<List<MarketOrder>> LoadMarketPrice(int regionId, int typeId, int page);
    }
}