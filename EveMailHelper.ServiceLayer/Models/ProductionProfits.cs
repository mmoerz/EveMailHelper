using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Models
{
    public class ProductionProfits
    {
        public ProductionProfits(ProductionCosts directProductionCosts, ProductionCosts bestPriceProductionCosts)
        {
            DirectProduction = new(directProductionCosts);
            BestPrice = new(bestPriceProductionCosts);
        }

        protected double _marketValue;
        public double MarketValue
        {
            get { return _marketValue; }

        }
        protected double _marketValueTaxes;
        public double MarketValueTaxes
        {
            get { return _marketValueTaxes; }
            set
            {
                _marketValueTaxes = value;
                DirectProduction.MarketValueTaxes = _marketValueTaxes;
                BestPrice.MarketValueTaxes = _marketValueTaxes;
            }
        }
        protected double _immediateSellValue;
        public double ImmediateSellValue
        {
            get { return _immediateSellValue; }

        }
        protected double _immediateSellTaxes;
        public double ImmediateSellTaxes
        {
            get { return _immediateSellValue; }
            set
            {
                _immediateSellTaxes = value;
                DirectProduction.ImmediateSellTaxes = _immediateSellTaxes;
                BestPrice.ImmediateSellTaxes = _immediateSellTaxes;
            }
        }

        public void SetMarketValue(double value)
        {
            _marketValue = value;
            DirectProduction.SetMarketValue(_marketValue);
            BestPrice.SetMarketValue(MarketValue);
        }

        public void SetImmediateSellValue(double value)
        {
            _immediateSellValue = value;
            DirectProduction.ImmediateSellValue = _immediateSellValue;
            BestPrice.ImmediateSellValue = _immediateSellValue;
        }

        public Profit DirectProduction { get; set; }
        public Profit BestPrice { get; set; }

    }
}
