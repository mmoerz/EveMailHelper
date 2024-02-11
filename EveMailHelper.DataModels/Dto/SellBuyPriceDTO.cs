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

        public double SellPrice { get; set;}
        public double BuyPrice { get; set;}

    }
}
