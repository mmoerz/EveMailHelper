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
using System.Security.Permissions;
using EveMailHelper.ServiceLayer.Utilities;

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

        #region parameters
        [Parameter]
        public ProductionPlan Plan { get; set; } = new();
        [Parameter]
        public double MaterialModifier { get; set; }
        #endregion

        public void Reload()
        {
            _table?.ReloadServerData();
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<BlueprintComponent>> ServerReload(TableState state)
        {
            TableData<BlueprintComponent> data = new();

            data.TotalItems = Plan.Count();
            data.Items = Plan;

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
            BlueprintAnalyzer blueprintAnalyzer = new(component, MaterialModifier);
            if (component.SubComponents.Count == 0)
                return Color.Primary;

            if (blueprintAnalyzer.IsBuyingComponentBetter())
                return Color.Success;
            else
                return Color.Secondary;
        }
    }
}
