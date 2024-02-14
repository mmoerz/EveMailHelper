using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Shared.EveChar;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using EveMailHelper.DataModels.Security;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.Web.Shared.Blueprints;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.DataModels.Market;
using EveMailHelper.Web.Shared.Market;
using EveMailHelper.ServiceLayer.Utilities;

namespace EveMailHelper.Web.Pages.Market
{
    public partial class ReactionOverview : ComponentBase
    {
        #region injections
        [Inject]
        IAuthenticationManager AuthenticationManager { get; set; } = null!;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Inject]
        IMapManager MapManager { get; set; } = null!;
        [Inject]
        IProductionManager ProductionManager { get; set; } = null!;
        
        #endregion

        #region parameters
        #endregion

        private BlueprintDetails? ProductionPlanDisplay { get; set; } = null!;

        private IndustryBlueprint selectedBlueprint { get; set; } = new();

        private ProductionPlan ProdPlan {  get; set; } = new();

        private BuyListDetails BuyListComponent { get; set; } = null!;
        private BuyList ToBuyList { get; set; } = new();

        private BuildPlan BuildPlanDetails = null!;

        private int RegionId { get; set; } = -1;
        private double SystemCostIndex { get; set; } = 4.51;
        private double StructureBonuses { get; set; } = 1;
        private double FacilityTax { get; set; } = 1;
        private bool IsAlphaClone { get; set; } = false;

        private int NumberOfRuns { get; set; } = 1;
        private int NumberOfRunsMin { get; set; } = 1;

        private double MaterialConsumption { get; set; } = -2.6;

        private async void BlueprintSelected(IndustryBlueprint blueprint)
        {
            _ = ProductionPlanDisplay ?? throw new NullReferenceException("Buildplan");

            try
            {
                if (blueprint != null && blueprint.TypeId != 0)
                {
                    selectedBlueprint.CopyShallow(blueprint);
                    var newplan = await ProductionManager.GetProductionPlan(
                        blueprint, new List<int>() { 11 },
                        RegionId, SystemCostIndex, StructureBonuses, FacilityTax, MaterialConsumption,
                        IsAlphaClone);
                    ProdPlan.ShallowCopy(newplan);
                    ProductionPlanAnalyzer analyzer = new(newplan);
                    NumberOfRunsMin = analyzer.GetMinNumberOfRuns(true);
                    if (NumberOfRuns < NumberOfRunsMin)
                        NumberOfRuns = NumberOfRunsMin;
                    var newBuyList = ProductionManager.DeriveBestPriceBuyListFromPlan(
                        newplan, 10, MaterialConsumption);
                    ToBuyList.CopyShallow(newBuyList);
                    BuyListComponent.RefreshTheFucker();
                    BuildPlanDetails.RefreshTheFucker();
                }
            }
            catch (Exception ex)
            {
                int x = 1;
            }

            ProductionPlanDisplay.Reload();
        }

        protected List<string> Reactions()
        {
            List<string> reactions = new();

            return reactions;
        }

        private Account Account { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            Account = AuthenticationManager.GetAccountFromPrincipal(user);

            var region = await MapManager.GetRegionByName("The Forge");
            _ = region ?? throw new Exception("Jita region not found");
            RegionId = region.EveId;
        }
    }
}
