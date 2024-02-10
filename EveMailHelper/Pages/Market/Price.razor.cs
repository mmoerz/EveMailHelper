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
using EveMailHelper.Web.Shared.Market;
using Microsoft.Extensions.Primitives;

namespace EveMailHelper.Web.Pages.Market
{
    public partial class Price : ComponentBase
    {
        #region injections
        [Inject]
        IAuthenticationManager AuthenticationManager { get; set; } = null!;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        [Inject]
        IMapManager MapManager { get; set; } = null!;
        [Inject]
        IEveTypeManager EveTypeManager { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        private MarketOrderList? myMarketOrder { get; set; } = null!;

        private IndustryBlueprint selectedBlueprint { get; set; } = new();
        private string selectedBlueprintName { get; set; } = "none";

        private void BlueprintSelected(IndustryBlueprint blueprint)
        {
            _ = myMarketOrder ?? throw new NullReferenceException("BlueprintDetails");

            selectedBlueprint.CopyShallow(blueprint);
            selectedBlueprintName = blueprint.Type.TypeName;

            myMarketOrder.Reload();
        }


        private string RegionName { get; set; } = string.Empty;
        private string EveTypeName {  get; set; } = string.Empty;
        private int RegionId { get; set; }
        private int TypeId { get; set; }

        public async Task<IEnumerable<string>> SearchRegion(string regionNamePartial)
        {
            if (regionNamePartial == null)
                return new List<string>();

            return await MapManager.SearchForRegionName(regionNamePartial);
        }

        public async Task<IEnumerable<string>> SearchEveType(string typeNamePartial)
        {
            if (typeNamePartial == null)
                return new List<string>();

            return await EveTypeManager.SearchForEveTypeName(typeNamePartial);
        }

        public async Task UpdateMarketPrice()
        {
            if (myMarketOrder == null)
                return;
            if (string.IsNullOrEmpty(RegionName) || string.IsNullOrEmpty(EveTypeName))
                return;

            var region = await MapManager.GetRegionByName(RegionName);
            if (region == null)
                throw new Exception($"Regionname {RegionName} does not exist");
            RegionId = region.EveId;
            var evetype = await EveTypeManager.GetByName(EveTypeName);
            if (evetype == null)
                throw new Exception($"EveType not found for name {EveTypeName}");
            TypeId = evetype.EveId;

            myMarketOrder.Reload();
        }


        private Account Account { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            Account = AuthenticationManager.GetAccountFromPrincipal(user);
        }
    }
}
