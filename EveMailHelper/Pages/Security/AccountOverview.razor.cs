using EveMailHelper.DataModels.Security;
using EveMailHelper.ServiceLayer.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

namespace EveMailHelper.Web.Pages.Security
{
    public partial class AccountOverview : ComponentBase
    {
        #region injections
        [Inject]
        IAuthenticationManager AuthenticationManager { get; set; } = null!;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        #region GUI Components
        #endregion

        private Account Account { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            Account = AuthenticationManager.GetAccountFromPrincipal(user);
        }


    }
}
