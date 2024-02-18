using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Models
{
    public class Profit
    {
        public Profit(ProductionCosts productionCosts)
        {
            ProductionCosts = productionCosts;
        }

        public ProductionCosts ProductionCosts { get; }

        protected double _marketValue;
        /// <summary>
        /// aka. realistic sellorder price - current minimum value of sellorders.
        /// </summary>
        /// <remarks>can be set via Profit.SetMarketValue</remarks>
        public double MarketValue { get { return _marketValue; } }
        public double MarketValueTaxes { get; set; }
        
        public double ImmediateSellValue { get; set; }
        public double ImmediateSellTaxes { get; set; }

        public double ProfitMarginSellOrder
        {
            get
            {
                return MarketValue - ProductionCosts.SumOfCosts() - MarketValueTaxes;
            }
        }
        
        public double ProfitMarginSellOrderInPercent
        {
            get
            {
                return ProfitMarginSellOrder / MarketValue / 100;
            }
        }

        public double ProfitMarginImmediateSell
        {
            get
            {
                return ImmediateSellValue - ProductionCosts.SumOfCosts() - ImmediateSellTaxes;
            }
        }
        public double ProfitMarginImmediateSellInPercent
        {
            get
            {
                return ProfitMarginImmediateSell / ImmediateSellValue / 100;
            }
        }

        public void SetMarketValue(double value)
        {
            _marketValue = value;
        }
    }
}
