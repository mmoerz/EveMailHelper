using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using MudBlazor;
using static MudBlazor.CategoryTypes;
using System.Linq;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Managers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EVEStandard.Models;

namespace EveMailHelper.Web.Shared.Blueprints
{
    public partial class BuildPlan : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        [Inject] IMarketManager MarketManager { get; set; } = null!;
        [Inject] IMapManager MapManager { get; set; } = null!;
        [Inject] IProductionManager ProductionManager { get; set; } = null!;
        #endregion

        //private MudTable<BlueprintComponent> _table = null!;
        private ProductionPlan _mainPlan = new ProductionPlan();

        #region parameters
        [Parameter]
        public ProductionPlan? Plan {  get; set; }
        // TODO Region is fixed, should be 'selectable'
        [Parameter]
        public int RegionId { get; set; } = -1;
        [Parameter]
        public int MaxAgeInMinutes { get; set; } = 60;
        [Parameter]
        public double SystemCostIndex { get; set; } = 4.46; // systemcostindex = %
        [Parameter]
        public double StructureBonuses { get; set; } =  1; // no bonus (1)
        [Parameter]
        public double FacilityTax { get; set; } = 1; // 1%

        [Parameter] 
        public RenderFragment<ProductionPlan> BuildPlanContent { get; set; } = default!;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            if  (RegionId <= 0) { 
                var region = await MapManager.GetRegionByName("The Forge");
                _ = region ?? throw new Exception("Jita region not found");
                RegionId = region.EveId;
            }
        }

        public void Reload()
        {
            //_table?.ReloadServerData();
        }

        public double SleepFucker = 1;

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        //private async Task<TableData<BlueprintComponent>> ServerReload(TableState state)
        //{
        //    TableData<BlueprintComponent> data = new();

        //    if (_blueprint != null && _blueprint.TypeId != 0)
        //    {
        //        // TODO: ugly ugly reference to use '11' as an activity filter directly
        //        _mainPlan = await BlueprintManager.GetBlueprintComponentsList(_blueprint, 11);
        //        if (_mainPlan.Product != null && _mainPlan.Product.EveId > 0)
        //        {
        //            var sellbuyPrice = await MarketManager.ArchivedBuySellPrice(RegionId, _mainPlan.Product.EveId, MaxAgeInMinutes);
        //            _mainPlan.ProductPrice = sellbuyPrice.SellPrice;
        //            foreach (var item in _mainPlan)
        //            {
        //                sellbuyPrice = await MarketManager.ArchivedBuySellPrice(RegionId, item.EveId, MaxAgeInMinutes);
        //                item.PricePerUnit = sellbuyPrice.SellPrice;
        //            }
        //            await ProductionManager.AddProductionCosts(
        //                _mainPlan, SystemCostIndex, StructureBonuses, FacilityTax, isAlphaClone: false);
        //        }
        //    }

        //    data.TotalItems = _mainPlan.SubComponents.Count();
        //    data.Items = _mainPlan;
        //    SleepFucker = _mainPlan.JobCost;

        //    return data;
        //}

        public string SomeFooBarText = "";
        public int count = 0;
        private void OnClickTest()
        {
            count++;
            SomeFooBarText = $"Button was pressed {count} times";
        }
    }
}
