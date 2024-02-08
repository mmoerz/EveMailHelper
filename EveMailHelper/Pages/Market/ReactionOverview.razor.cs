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
        IBlueprintManager BlueprintManager { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        private BlueprintDetails? ListOfComponents { get; set; } = null!;

        private IndustryBlueprint selectedBlueprint { get; set; } = new();
        private string selectedBlueprintName { get; set; } = "none";

        private void BlueprintSelected(IndustryBlueprint blueprint)
        {
            _ = ListOfComponents ?? throw new NullReferenceException("BlueprintDetails");

            selectedBlueprint.CopyShallow(blueprint);
            selectedBlueprintName = blueprint.Type.TypeName;

            ListOfComponents.Reload();
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
        }
    }
}
