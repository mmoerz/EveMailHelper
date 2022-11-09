using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.BusinessLibrary.Tools;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Pages.EveMails
{
    public partial class ManualEveMail : ComponentBase
    {
        #region injections
        [Inject] ISnackbar Snackbar { get; set; } = null!;
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;
        // maybe of use when coloring the already known chars
        //[Inject] ICharacterService CharacterService { get; set; } = null!;
        [Inject] IEveMailService EveMailService { get; set; } = null!;

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
            var receivers = ReceiverString.SplitStringOfCharacters(',');
            await EveMailService.SendTo(TemplateId, receivers);
            Snackbar.Add("Message saved as sent");
        }
    }
}
