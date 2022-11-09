using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Web.Shared.Notes
{
    public partial class NoteDialog : ComponentBase
    {
        #region injections
        [Inject] INoteService NoteService { get; set; } = null!;
        #endregion

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public Note Model { get; set; } = new();
        [Parameter]
        public EventCallback<Note> DialogSaved { get; set; }
        #endregion

        private MudForm? form = null!;
        
        private async Task Save()
        {
            _ = form ?? throw new NullReferenceException("form must not be null");
            await form.Validate();

            if (form.IsValid)
            {
                // Notify parent component to
                // submit the changed Analysisrequest
                await NoteService.Update(Model);
                await DialogSaved.InvokeAsync(Model);
            }
            
            MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

    }
}
