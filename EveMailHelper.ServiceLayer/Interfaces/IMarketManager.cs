using EVEStandard.Models;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IMarketManager
    {
        Task<List<MarketOrder>> LoadMarketPrice(int regionId, int typeId, int page);
    }
}