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

        private ProductionPlan productionPlan {  get; set; } = new();

        private int RegionId { get; set; } = -1;
        private double SystemCostIndex { get; set; } = 4.51;
        private double StructureBonuses { get; set; } = 1;
        private double FacilityTax { get; set; } = 1;
        private bool IsAlphaClone { get; set; } = false;

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
                        RegionId, SystemCostIndex, StructureBonuses, FacilityTax, IsAlphaClone);
                    productionPlan.ShallowCopy(newplan);
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
