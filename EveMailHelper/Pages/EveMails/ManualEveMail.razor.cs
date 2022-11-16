using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace EveMailHelper.Web.Pages.EveMails
{
    public partial class ManualEveMail : ComponentBase
    {
        #region injections
        [Inject] ISnackbar Snackbar { get; set; } = null!;
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;
        // maybe of use when coloring the already known chars
        //[Inject] ICharacterService CharacterService { get; set; } = null!;
        [Inject] IEveMailService EveMailService { get; set; } = null!;

        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Inject] IAuthenticationManager AuthenticationManager { get; set; } = null!;

        #endregion

        #region parameters
        #endregion

        #region GUI Components

        #endregion

        static Guid defaultValue = Guid.Parse("6d5fa73f7a8c4f09d59408dab9e7fc3c");
        private string ReceiverString { get; set; } = null!;
        private Guid TemplateId { get; set; } = defaultValue;
        private ICollection<EveMailTemplate> templates = null!;

        protected override async Task OnInitializedAsync()
        {
            //dbContext = await dbContextFactory.CreateDbContextAsync();
            templates = await EveMailTemplateService.GetAll();
        }

        private void Cancel()
        {
            ReceiverString = "";
        }

        private async Task Submit()
        {
            //if (TemplateId == null)
            //    return;
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            var character = await AuthenticationManager.GetCharacterFromPrincipal(user);

            var receivers = ReceiverString.SplitStringOfCharacters(',');
            await EveMailService.SendTo(TemplateId, character,  receivers);
            Snackbar.Add("Message saved as sent");
        }
    }
}
