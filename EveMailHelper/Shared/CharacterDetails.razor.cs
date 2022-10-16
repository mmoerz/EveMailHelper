using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Shared
{
    public partial class CharacterDetails : ComponentBase
    {
        #region injections
        [Inject] ICharacterService CharacterService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public Character Model { get; set; }
            //get { _character.CreatedDate = date ?? DateTime.Now; return _character; } 
            //set { _character = value; date = _character.CreatedDate; } 
        //}
        //private Character _character = null!;

        [Parameter]
        public EventCallback<Character> OnCharacterSave { get; set; }
        #endregion

        // a mudblazor hack, since the datetimepicker requires a nullable var
        private DateTime? date //= DateTime.Now;
        {
            get { return Model.CreatedDate; }
            set { Model.CreatedDate = value ?? DateTime.Now; }
        }

        public bool IsEditable { get; set; } = false;
        private Character _backup = null!;

        public void Cancel()
        {
            // ok this is not optimal ...
            //_character.CopyShallow(_backup);
            Model.CopyShallow(_backup);
            //date = _character.CreatedDate;
            IsEditable = false;
        }

        public void Edit()
        {
            _backup = Model;
            IsEditable = true;
        }

        public async Task Save()
        {            
            await CharacterService.Update(Model);
            await OnCharacterSave.InvokeAsync(Model);
            IsEditable = false;
        }
    }
}
