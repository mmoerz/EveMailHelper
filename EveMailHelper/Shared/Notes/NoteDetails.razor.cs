using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Shared.Notes
{
    public partial class NoteDetails : ComponentBase
    {
        #region injections
        [Inject] ISnackbar Snackbar { get; set; } = null!;
        [Inject] INoteService NoteService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter] 
        public Note Note { get; set; } = null!;

        [Parameter]
        public EventCallback<Note> OnNoteSave { get; set; }

        [Parameter]
        public bool ButtonsEnabled { get; set; } = true;

        [Parameter]
        public bool IsEditable { get; set; } = false;
        #endregion

        // a mudblazor hack, since the datetimepicker requires a nullable var
        private DateTime? Date //= DateTime.Now;
        {
            get { return Note.CreatedDate; }
            set { Note.CreatedDate = value ?? DateTime.Now; }
        }
        
        private Note _backup = null!;

        public void Cancel()
        {
            Note.CopyShallow(_backup);
            IsEditable = false;
        }

        public void Edit()
        {
            _backup = Note;
            IsEditable = true;
        }

        public async Task Save()
        {
            try
            {
                await NoteService.Update(Note);
                await OnNoteSave.InvokeAsync(Note);
                IsEditable = false;
            }
            catch
            {
                Snackbar.Add("Failed to Add Note", Severity.Error);
            }
        }
    }
}
