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

        public void RefreshTheFucker()
        {
            StateHasChanged();
        }
    }
}
