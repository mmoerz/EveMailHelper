using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Shared.Notes
{
    public partial class NoteDetails : ComponentBase
    {
        #region injections
        [Inject] INoteService NoteService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter] 
        public Note Model { get; set; } = null!;

        [Parameter]
        public EventCallback<Note> OnNoteSave { get; set; }
        #endregion

        // a mudblazor hack, since the datetimepicker requires a nullable var
        private DateTime? Date //= DateTime.Now;
        {
            get { return Model.CreatedDate; }
            set { Model.CreatedDate = value ?? DateTime.Now; }
        }

        public bool IsEditable { get; set; } = false;
        private Note _backup = null!;

        public void Cancel()
        {
            Model.CopyShallow(_backup);
            IsEditable = false;
        }

        public void Edit()
        {
            _backup = Model;
            IsEditable = true;
        }

        public async Task Save()
        {            
            await NoteService.Update(Model);
            await OnNoteSave.InvokeAsync(Model);
            IsEditable = false;
        }
    }
}
