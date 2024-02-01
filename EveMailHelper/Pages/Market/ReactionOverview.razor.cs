using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Shared.EveChar;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.Web.Pages.EveChar
{
    public partial class ReactionOverview : ComponentBase
    {
        #region injections
        [Inject]
        IAuthenticationManager AuthenticationManager { get; set; } = null!;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        

        #endregion

        #region parameters
        #endregion

        


        
        /*
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
        */

        protected List<string> Reactions()
        {
            List<string> reactions = new List<string>();

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
