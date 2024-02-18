using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Market;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.ServiceLayer.Models
{
    public class ProductionCostExtended
    {
        public ProductionCostExtended()
        {
            DirectProduction = new();
            BestPriceProduction = new();
            Profit = new(DirectProduction, BestPriceProduction);
        }

        //public int EveTypeId { get; set; }
        //public int ActivityId { get; set; }

        //public int ProductId { get; set; }
        public int NumberOfRuns { get; set; }

        public int ProductQuantity { get; set; }

        public ProductionCosts DirectProduction { get; set; }
        public ProductionCosts BestPriceProduction { get; set; }

        public ProductionProfits Profit { get; set; }

        public DateTime LastUpdatedFromEve { get; set; } = DateTime.UtcNow;
        public virtual EveType EveType { get; set; } = null!;
        public virtual IndustryActivity IndustryActivity { get; set; } = null!;
        public virtual EveType Product { get; set; } = null!;

        public void SetProductSellPricePerUnit(double value)
        {
            Profit.SetMarketValue(value * ProductQuantity * NumberOfRuns);
        }
        public double GetProductSellPricePerUnit()
        {
            return Profit.MarketValue / ProductQuantity / NumberOfRuns;
        }

        public void SetProductBuyPricePerUnit(double value)
        {
            Profit.SetImmediateSellValue(value * ProductQuantity * NumberOfRuns);
        }
        public double GetProductBuyPricePerUnit()
        {
            return Profit.ImmediateSellValue / ProductQuantity / NumberOfRuns;
        }
        public ProductionCostExtended CopyDeep(NormalizedProductionCost cost)
        {
            NumberOfRuns = cost.NumberOfRuns;

            DirectProduction.JobCosts = cost.DirectJobCost;
            DirectProduction.ComponentCosts = cost.DirectComponentCost;
            BestPriceProduction.JobCosts = cost.BestPriceJobCost;
            BestPriceProduction.ComponentCosts = cost.BestPriceComponentCost;

            ProductQuantity = cost.ProductQuantity;

            SetProductSellPricePerUnit(cost.ProductSellPricePerUnit);
            SetProductBuyPricePerUnit(cost.ProductBuyPricePerUnit);

            LastUpdatedFromEve = cost.LastUpdatedFromEve;

            EveType = cost.EveType;
            IndustryActivity = cost.IndustryActivity;
            Product = cost.Product;

            return this;
        }

        public ProductionCostExtended CopyDeep(ProductionCostExtended cost)
        {
            NumberOfRuns = cost.NumberOfRuns;

            DirectProduction.JobCosts = cost.DirectProduction.JobCosts;
            DirectProduction.ComponentCosts = cost.DirectProduction.ComponentCosts;
            BestPriceProduction.JobCosts = cost.BestPriceProduction.JobCosts;
            BestPriceProduction.ComponentCosts = cost.DirectProduction.ComponentCosts;

            ProductQuantity = cost.ProductQuantity;

            Profit.SetMarketValue(cost.Profit.MarketValue);
            Profit.SetImmediateSellValue(cost.Profit.ImmediateSellValue);
            Profit.DirectProduction.MarketValueTaxes = cost.Profit.DirectProduction.MarketValueTaxes;
            Profit.DirectProduction.ImmediateSellTaxes = cost.Profit.DirectProduction.ImmediateSellTaxes;
            Profit.BestPrice.MarketValueTaxes = cost.Profit.BestPrice.MarketValueTaxes;
            Profit.BestPrice.ImmediateSellTaxes = cost.Profit.BestPrice.ImmediateSellTaxes;
            
            LastUpdatedFromEve = cost.LastUpdatedFromEve;

            EveType = cost.EveType;
            IndustryActivity = cost.IndustryActivity;
            Product = cost.Product;

            return this;
        }

        public NormalizedProductionCost ToNormalizedProductionCost()
        {
            return new NormalizedProductionCost()
            {
                NumberOfRuns = NumberOfRuns,
                DirectJobCost = DirectProduction.JobCosts,
                DirectComponentCost = DirectProduction.ComponentCosts,
                BestPriceJobCost = BestPriceProduction.JobCosts,
                BestPriceComponentCost = BestPriceProduction.ComponentCosts,
                ProductQuantity = ProductQuantity,
                ProductSellPricePerUnit = GetProductSellPricePerUnit(),
                ProductBuyPricePerUnit = GetProductBuyPricePerUnit(),
                LastUpdatedFromEve = LastUpdatedFromEve,
                EveType = EveType,
                IndustryActivity = IndustryActivity,
                Product = Product,
            };
        }
    }
}
