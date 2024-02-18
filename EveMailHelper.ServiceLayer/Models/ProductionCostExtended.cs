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
        //public int EveTypeId { get; set; }
        //public int ActivityId { get; set; }

        //public int ProductId { get; set; }
        public int NumberOfRuns { get; set; }

        public int ProductQuantity { get; set; }

        public ProductionProfits Profit { get; set; } = new ProductionProfits();

        public DateTime LastUpdatedFromEve { get; set; } = DateTime.UtcNow;
        public virtual EveType EveType { get; set; } = null!;
        public virtual IndustryActivity IndustryActivity { get; set; } = null!;
        public virtual EveType Product { get; set; } = null!;

        public void CopyShallow(NormalizedProductionCost cost)
        {
            EveType = cost.EveType;
            IndustryActivity = cost.IndustryActivity;
            NumberOfRuns = cost.NumberOfRuns;
            ProductQuantity = cost.ProductQuantity;
            LastUpdatedFromEve = cost.LastUpdatedFromEve;
            Profit.MarketValue = cost.ProductCostSum;
            Profit.DirectProduction.ProductionCost = cost.DirectCostSum;
            Profit.BestPrice.ProductionCost = cost.BestPriceSum;
        }
    }
}
