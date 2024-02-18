using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Dto
{
    public class SellBuyPriceDTO
    {
        public SellBuyPriceDTO() { }
        public SellBuyPriceDTO(double sellPrice, double buyPrice) 
        {
            SellPrice = sellPrice;
            BuyPrice = buyPrice;
        }

        /// <summary>
        /// realistic price minimum for a sell order to set
        /// </summary>
        /// <remarks>is the current min value found in the market orders</remarks>
        public double SellPrice { get; set;}
        /// <summary>
        /// realistic price to set for a buy order
        /// </summary>
        /// <remarks>is the current max value found in the market orders</remarks>
        public double BuyPrice { get; set;}

    }
}
