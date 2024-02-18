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
using MudBlazor;
using Microsoft.Extensions.Hosting;

namespace EveMailHelper.Web.Pages.Market
{
    public partial class ReactionAnalysis : ComponentBase
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
        [Inject]
        IBlueprintManager BlueprintManager { get; set; } = null!;
        [Inject]
        ITaxManager TaxManager { get; set; } = null!;

        #endregion

        #region parameters
        #endregion

        #region displayed components
        private MudNumericField<int> NumberOfRunsField { get; set; } = null!;
        private BlueprintDetails? ProductionPlanDisplay { get; set; } = null!;
        private BuildPlan BuildPlanDetails = null!;
        private BuyListDetails BuyListComponent { get; set; } = null!;
        private BuyListTitle BuyListTitleComponent { get; set; } = null!;
        private ProductionCostDetails ProductionCostDetails { get; set; } = null!;
        #endregion

        private IndustryBlueprint selectedBlueprint { get; set; } = new();

        private ProductionPlan ProdPlan {  get; set; } = new();

        private BuyList ToBuyList { get; set; } = new();

        private ProductionCostExtended NormalizedProdCost = new();

        private int RegionId { get; set; } = -1;
        private double SystemCostIndex { get; set; } = 4.51;
        private double StructureBonuses { get; set; } = 1;
        private double FacilityTax { get; set; } = 1;
        private bool IsAlphaClone { get; set; } = false;

        private int NumberOfRuns { get; set; } = 1;
        private int NumberOfRunsMin { get; set; } = 1;

        private int AccountSkillLevel { get; set; } = 5;
        private int BrokerRelationsLevel { get; set; } = 5;
        private double FactionStanding { get; set; } = 1.0;
        private double CorpStanding { get; set; } = 0;

        private double MaterialConsumption { get; set; } = -2.6;

        private async void BlueprintSelected(ProductionCostExtended cost)
        {
            _ = ProductionPlanDisplay ?? throw new NullReferenceException("Buildplan");

            try
            {
                if (cost != null && cost.EveType != null && cost.EveType.EveId != 0)
                {
                    // npc --> blueprint ??
                    //
                    var blueprint = await BlueprintManager.GetBlueprint(cost.EveType.EveId);
                    if (blueprint != null)
                    {
                        var newplan = await ProductionManager.GetProductionPlan(
                            blueprint, new List<int>() { 11 },
                            RegionId, SystemCostIndex, StructureBonuses, FacilityTax, MaterialConsumption,
                            IsAlphaClone);
                        ProdPlan.ShallowCopy(newplan);
                        ProductionPlanAnalyzer analyzer = new(newplan, MaterialConsumption);
                        NumberOfRunsMin = analyzer.GetMinNumberOfRuns(true);
                        if (NumberOfRuns < NumberOfRunsMin)
                            NumberOfRuns = NumberOfRunsMin;
                        var newBuyList = ProductionManager.DeriveBestPriceBuyListFromPlan(
                            newplan, NumberOfRuns, MaterialConsumption);
                        ToBuyList.CopyShallow(newBuyList);
                        var newprodcost = ProductionManager.DeriveProductionCostWithTax(
                            newplan, NumberOfRuns, MaterialConsumption, AccountSkillLevel, BrokerRelationsLevel,
                            FactionStanding, CorpStanding
                            );
                        NormalizedProdCost.CopyDeep(newprodcost);
                        NormalizedProdCost.EveType = cost.EveType;
                        RefreshSubComponents();
                    }
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

        protected void RefreshSubComponents()
        {
            if (BuyListTitleComponent != null) 
                BuyListTitleComponent.Refresh();
            if (BuyListComponent != null)
                BuyListComponent.RefreshTheFucker();
            if (BuildPlanDetails != null)
                BuildPlanDetails.RefreshTheFucker();
            if (ProductionCostDetails != null)
                ProductionCostDetails.Refresh();
        }

        public async Task OnNumberOfRunsChanged(int newValue) 
        {
            NumberOfRuns = newValue;
            if (NormalizedProdCost == null || NormalizedProdCost.EveType == null)
                return;

            var blueprint = await BlueprintManager.GetBlueprint(NormalizedProdCost.EveType.EveId);
            if (blueprint != null)
            {
                var newplan = await ProductionManager.GetProductionPlan(
                    blueprint, new List<int>() { 11 },
                    RegionId, SystemCostIndex, StructureBonuses, FacilityTax, MaterialConsumption,
                    IsAlphaClone);
                ProdPlan.ShallowCopy(newplan);
                ProductionPlanAnalyzer analyzer = new(newplan, MaterialConsumption);
                NumberOfRunsMin = analyzer.GetMinNumberOfRuns(true);
                if (NumberOfRuns < NumberOfRunsMin)
                    NumberOfRuns = NumberOfRunsMin;
                var newBuyList = ProductionManager.DeriveBestPriceBuyListFromPlan(
                    newplan, NumberOfRuns, MaterialConsumption);
                ToBuyList.CopyShallow(newBuyList);
                var newprodcost = ProductionManager.DeriveProductionCostWithTax(
                       newplan, NumberOfRuns, MaterialConsumption, AccountSkillLevel, BrokerRelationsLevel,
                       FactionStanding, CorpStanding
                       );
                NormalizedProdCost.CopyDeep(newprodcost);
                RefreshSubComponents();
            }
        }
    }
}
