using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Market;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class MapMarketPriceExtension
    {
        public static void MapFrom(this MarketPrice marketOrder, EVEStandard.Models.MarketPrice mapFrom)
        {
            marketOrder.EveTypeId = mapFrom.TypeId;
            marketOrder.AdjustedPrice = mapFrom.AdjustedPrice ?? 0.0;
            marketOrder.AveragePrice = mapFrom.AveragePrice ?? 0.0;
        }

        public static List<MarketPrice> MapToMarketPriceList<T>(this IList<T> list) 
            where T : EVEStandard.Models.MarketPrice
        {
            List<MarketPrice> result  = new List<MarketPrice>();

            foreach (T item in list)
            {
                var order = new MarketPrice();
                order.MapFrom(item);
                result.Add(order);
            }
            return result;
        }
    }
}
