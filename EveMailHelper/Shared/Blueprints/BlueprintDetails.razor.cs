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
    public partial class BlueprintDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        [Inject] IMarketManager MarketManager { get; set; } = null!;
        [Inject] IMapManager MapManager { get; set; } = null!;
        [Inject] IProductionManager ProductionManager { get; set; } = null!;
        #endregion

        private MudTable<BlueprintComponent> _table = null!;
        private ProductionPlan _mainPlan = new ProductionPlan();
        private IndustryBlueprint _blueprint = new();

        #region parameters
        [Parameter]
        public IndustryBlueprint Blueprint
        {
            get
            {
                return _blueprint;
            }
            set 
            {
                _blueprint = value;
            }
        }

        [Parameter]
        public string BlueprintName { get; set; } = "no Blueprint";


        #endregion

        // TODO Region is fixed, should be 'selectable'
        private int RegionId = -1;
        private int MaxAgeInMinutes = 60;
        private double SystemCostIndex = 4.46; // systemcostindex = %
        private double StructureBonuses = 1; // no bonus (1)
        private double FacilityTax = 1; // 1%

        protected override async Task OnInitializedAsync()
        {
            var region = await MapManager.GetRegionByName("The Forge");
            _ = region ?? throw new Exception("Jita region not found");
            RegionId = region.EveId;
        }

        public void Reload()
        {
            _table?.ReloadServerData();
        }

        public double SleepFucker = 1;

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<BlueprintComponent>> ServerReload(TableState state)
        {
            TableData<BlueprintComponent> data = new();

            try { 
            if (_blueprint != null && _blueprint.TypeId != 0)
            {
                _mainPlan = await ProductionManager.GetProductionPlan(_blueprint, new List<int>() { 11 },
                    RegionId, SystemCostIndex, StructureBonuses, FacilityTax, isAlphaClone: false);

                //// TODO: ugly ugly reference to use '11' as an activity filter directly
                //_mainPlan = await BlueprintManager.GetBlueprintComponentsList(_blueprint, 11);
                //if (_mainPlan.Product != null && _mainPlan.Product.EveId > 0)
                //{
                //    var sellbuyPrice = await MarketManager.ArchivedBuySellPrice(RegionId, _mainPlan.Product.EveId, MaxAgeInMinutes);
                //    _mainPlan.ProductPrice = sellbuyPrice.SellPrice;
                //    foreach (var item in _mainPlan)
                //    {
                //        sellbuyPrice = await MarketManager.ArchivedBuySellPrice(RegionId, item.EveId, MaxAgeInMinutes);
                //        item.PricePerUnit = sellbuyPrice.SellPrice;
                //    }
                //    await ProductionManager.AddProductionCosts(
                //        _mainPlan, SystemCostIndex, StructureBonuses, FacilityTax, isAlphaClone: false);
                //}
            }
            } catch(Exception ex)  
            {
                int x = 1;
            }

            data.TotalItems = _mainPlan.SubComponents.Count();
            data.Items = _mainPlan;
            SleepFucker = _mainPlan.JobCost;

            return data;
        }

        private string GetClassForDepth(int depth)
        {
            if (depth == 1)
                return "";
            return string.Format("ml-{0}", (depth-1)*2);
        }

        private MudBlazor.Color GetColorForPriceSum(BlueprintComponent component)
        {
            if (component.SubComponents.Count == 0)
                return Color.Primary;

            if (component.IsProducingBetter)
                return Color.Success;
            else
                return Color.Secondary;
        }

        public string SomeFooBarText = "";
        public int count = 0;
        private void OnClickTest()
        {
            count++;
            SomeFooBarText = $"Button was pressed {count} times";
        }
    }
}
