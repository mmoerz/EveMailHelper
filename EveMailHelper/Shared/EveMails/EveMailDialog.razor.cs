using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Web.Shared.EveMails
{
    public partial class EveMailDialog : ComponentBase
    {
        #region injections
        [Inject] IMailService EveMailService { get; set; } = null!;
        #endregion

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public bool Editable { get; set; } = false;
        [Parameter]
        public Mail Model { get; set; } = new();
        [Parameter]
        public EventCallback<Mail> DialogSaved { get; set; }
        #endregion

        MudForm? form = null!;

        Color Color = Color.Success;

        private async Task Save()
        {
            //await form.Validate();

            //if (form.IsValid)
            {
                // Notify parent component to
                // submit the changed Analysisrequest
                EveMailService.Update(Model);
                await DialogSaved.InvokeAsync(Model);
            }

            MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

        MarkupString ModelContentMarkup => (MarkupString)Model.Content
            .Replace("<font size=\"13\"", "<font size=\"3\"")
            .Replace("<font size=\"18\"", "<font size=\"4\"");
        
    }
}
