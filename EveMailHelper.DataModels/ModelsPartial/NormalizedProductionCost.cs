namespace EveMailHelper.DataModels.Market
{
    public partial class NormalizedProductionCost
    {
        public void CopyShallow(NormalizedProductionCost cost)
        {
            EveTypeId = cost.EveTypeId;
            EveType = cost.EveType;
            ActivityId = cost.ActivityId;
            IndustryActivity = cost.IndustryActivity;
            NumberOfRuns = cost.NumberOfRuns;
            DirectJobCost = cost.DirectJobCost;
            DirectComponentCost = cost.DirectComponentCost;
            DirectCostSum = cost.DirectCostSum;
            BestPriceJobCost = cost.BestPriceJobCost;
            BestPriceComponentCost = cost.BestPriceComponentCost;
            BestPriceSum = cost.BestPriceSum;
            ProductId = cost.ProductId;
            ProductQuantity = cost.ProductQuantity;
            ProductPricePerUnit = cost.ProductPricePerUnit;
            ProductCostSum = cost.ProductCostSum;
            LastUpdatedFromEve = cost.LastUpdatedFromEve;
            Product = cost.Product;
        }
    }
}
