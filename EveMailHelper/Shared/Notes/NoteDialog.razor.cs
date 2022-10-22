using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace EveMailHelper.Shared.Notes
{
    public partial class NoteDialog : ComponentBase
    {
        #region injections
        [Inject] INoteService NoteService { get; set; } = null!;
        #endregion

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [CascadingParameter]
        public Note Model { get; set; } = new();
        [Parameter]
        public EventCallback<Note> DialogSaved { get; set; }
        #endregion

        MudForm? form = null!;
        
        private async Task Save()
        {
            //await form.Validate();

            //if (form.IsValid)
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
