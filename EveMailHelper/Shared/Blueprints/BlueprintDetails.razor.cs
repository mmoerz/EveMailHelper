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
        //private ProductionPlan _mainPlan = new ProductionPlan();
        //private IndustryBlueprint _blueprint = new();
        private BuildPlan BuildPlanDetails = null!;

        #region parameters
        //[Parameter]
        //public IndustryBlueprint Blueprint
        //{
        //    get
        //    {
        //        return _blueprint;
        //    }
        //    set 
        //    {
        //        _blueprint = value;
        //    }
        //}

        //[Parameter]
        //public string BlueprintName { get; set; } = "no Blueprint";

        [Parameter]
        public ProductionPlan Plan { get; set; } = new(); 
        #endregion

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

            data.TotalItems = Plan.Count();
            data.Items = Plan;
            SleepFucker = Plan.JobCost;

            if (BuildPlanDetails != null)
                BuildPlanDetails.RefreshTheFucker();

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
