namespace EveMailHelper.DataModels.Market
{
    public partial class NormalizedProductionCost
    {
        public void CopyShallow(NormalizedProductionCost cost)
        {
            EveType = cost.EveType;
            IndustryActivity = cost.IndustryActivity;
            NumberOfRuns = cost.NumberOfRuns;
            DirectJobCost = cost.DirectJobCost;
            DirectComponentCost = cost.DirectComponentCost;
            BestPriceJobCost = cost.BestPriceJobCost;
            BestPriceComponentCost = cost.BestPriceComponentCost;
            ProductQuantity = cost.ProductQuantity;
            ProductSellPricePerUnit = cost.ProductSellPricePerUnit;
            LastUpdatedFromEve = cost.LastUpdatedFromEve;
            Product = cost.Product;
        }
    }
}
