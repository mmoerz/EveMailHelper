using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;

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
        public EveMailTemplate model { get; set; } = new();
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
                await EveMailTemplateService.AddOrUpdate(model);
                await DialogSaved.InvokeAsync(model);
            }
            
            MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

    }
}
