using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace EveMailHelper.Shared
{
    public partial class EveMailTemplateDialog : ComponentBase
    {
        #region injections
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;
        #endregion

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public EveMailTemplate Model { get; set; } = new();
        [Parameter]
        public EventCallback<EveMailTemplate> DialogSaved { get; set; }
        #endregion

        MudForm form = null!;
        
        private async Task Save()
        {
            //await form.Validate();

            //if (form.IsValid)
            {
                // Notify parent component to
                // submit the changed Analysisrequest
                EveMailTemplateService.AddOrUpdate(Model);
                await DialogSaved.InvokeAsync(Model);
            }
            
            MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

    }
}
