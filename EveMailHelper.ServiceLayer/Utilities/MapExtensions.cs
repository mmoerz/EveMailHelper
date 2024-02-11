using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Market;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class MapExtensions
    {
        public static void MapFrom(this MarketOrder marketOrder, EVEStandard.Models.MarketOrder mapFrom)
        {
            marketOrder.EveId = mapFrom.OrderId;
            marketOrder.Duration = mapFrom.Duration;
            marketOrder.IsBuyOrder = mapFrom.IsBuyOrder;
            marketOrder.Issued = mapFrom.Issued;
            marketOrder.LocationId = mapFrom.LocationId;
            marketOrder.MinVolume = mapFrom.MinVolume;
            marketOrder.Price = mapFrom.Price;
            marketOrder.Range = mapFrom.Range;
            marketOrder.SolarSystemId = mapFrom.SystemId;
            marketOrder.TypeId = mapFrom.TypeId;
            marketOrder.VolumeRemain = mapFrom.VolumeRemain;
            marketOrder.VolumeTotal = mapFrom.VolumeTotal;
        }

        public static List<MarketOrder> MapToMarketOrderList<T>(this IList<T> list) 
            where T : EVEStandard.Models.MarketOrder
        {
            List<MarketOrder> result  = new List<MarketOrder>();

            foreach (T item in list)
            {
                MarketOrder order = new MarketOrder();
                order.MapFrom(item);
                result.Add(order);
            }
            return result;
        }
    }
}
