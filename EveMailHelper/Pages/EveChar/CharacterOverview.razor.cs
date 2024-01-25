using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Shared.EveChar;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.Web.Pages.EveChar
{
    public partial class CharacterOverview : ComponentBase
    {
        #region injections
        [Inject]
        IAuthenticationManager AuthenticationManager { get; set; } = null!;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        public CharacterList? ListOfCharacters = null!;

        //public Character Model { get; set; } = new();

        private CharacterDetails? CharDetails { get; set; } = null!;

        private void CharacterSelected(Character character)
        {
            _ = CharDetails ?? throw new NullReferenceException("CharDetails");
            CharDetails.SetModel(character);
        }

        private void CharacterChanged(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            ListOfCharacters?.Reload();
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
