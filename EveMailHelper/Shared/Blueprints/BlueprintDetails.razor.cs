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

namespace EveMailHelper.Web.Shared.Blueprints
{
    public partial class BlueprintDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        [Inject] IMarketManager MarketManager { get; set; } = null!;
        [Inject] IMapManager MapManager { get; set; } = null!;
        #endregion

        private MudTable<BlueprintComponents> _table = null!;
        private BlueprintComponents _mainProduct = new BlueprintComponents();
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

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<BlueprintComponents>> ServerReload(TableState state)
        {
            TableData<BlueprintComponents> data = new();

            if (_blueprint != null && _blueprint.TypeId != 0)
            {
                // TODO: ugly ugly reference to use '11' as an activity filter directly
                _mainProduct = await BlueprintManager.GetBlueprintComponentsList(_blueprint, 11);
                //_blueprintComponents = _mainProduct.SubComponents.ToList();
                //_blueprintComponents.ToFlatList();
            }

            data.TotalItems = _mainProduct.Count();
            //_blueprintComponents.Count; 
            data.Items = _mainProduct;//_blueprintComponents;

            if (_mainProduct.EveId > 0) { 
                // Todo: hmm, check is fine but we are splitting stuff all over here
                foreach (var item in data.Items)
                {
                    var sellbuyPrice = await MarketManager.ArchivedBuySellPrice(RegionId, item.EveId, MaxAgeInMinutes);
                    item.PricePerUnit = sellbuyPrice.SellPrice;
                                        
                }
            }

            return data;
        }

        private string GetClassForDepth(int depth)
        {
            if (depth == 1)
                return "";
            return string.Format("ml-{0}", (depth-1)*2);
        }

        private MudBlazor.Color GetColorForPriceSum(BlueprintComponents component)
        {
            if (component.SubComponents.Count == 0)
                return Color.Primary;

            if (component.IsProducingBetter)
                return Color.Success;
            else
                return Color.Secondary;
        }

        public void SetModel(BlueprintComponents value)
        {
            _mainProduct = value;
        }
    }
}
