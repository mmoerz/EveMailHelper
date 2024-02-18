using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Models
{
    public class Profit
    {
        public double ProductionCost { get; set; }

        protected double _marketValue;
        /// <summary>
        /// aka. realistic sellorder price - current minimum value of sellorders
        /// </summary>
        public double MarketValue { get { return _marketValue; } }
        public double MarketValueTaxes { get; set; }
        
        public double ImmediateSellValue { get; set; }
        public double ImmediateSellTaxes { get; set; }

        public double ProfitMarginSellOrder
        {
            get
            {
                return MarketValue - ProductionCost - MarketValueTaxes;
            }
        }

        public double ProfitMarginImmediateSell
        {
            get
            {
                return ImmediateSellValue - ProductionCost - ImmediateSellTaxes;
            }
        }

        public void SetMarketValue(double value)
        {
            _marketValue = value;
        }
    }
}
