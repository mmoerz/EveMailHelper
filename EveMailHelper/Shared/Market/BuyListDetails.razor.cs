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
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.Web.Shared.Market
{
    public partial class BuyListDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        [Inject] IMarketManager MarketManager { get; set; } = null!;
        [Inject] IMapManager MapManager { get; set; } = null!;
        [Inject] IProductionManager ProductionManager { get; set; } = null!;
        #endregion

        //private MudTable<BlueprintComponent> _table = null!;
        //private ProductionPlan _mainPlan = new ProductionPlan();

        #region parameters
        [Parameter]
        public BuyList? BList {  get; set; }
        // TODO Region is fixed, should be 'selectable'
        
        [Parameter] 
        public RenderFragment<BuyList> BuyListContent { get; set; } = default!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// yeah, i know, however it ate my nerves
        /// maybe i will correct this name, but most likely not
        /// </summary>
        public void RefreshTheFucker()
        {
            StateHasChanged();
        }
    }
}
