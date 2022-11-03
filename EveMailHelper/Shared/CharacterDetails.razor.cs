using EveMailHelper.BusinessLibrary.Interfaces;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;
using EveMailHelper.Models;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Shared
{
    public partial class CharacterDetails : ComponentBase
    {
        #region injections
        [Inject] ICharacterService CharacterService { get; set; } = null!;
        #endregion

        private Character _model = null!;

        #region parameters
        [Parameter]
        public Character Model
        {
            get
            {
                _model.CopyShallow(ViewModel);
                return _model;
            }
            set 
            {
                _model = value;
                ViewModel.CopyShallow(value);
            }
        }
        //get { _character.CreatedDate = date ?? DateTime.Now; return _character; } 
        //set { _character = value; date = _character.CreatedDate; } 
        //}
        //private Character _character = null!;
        

        [Parameter]
        public EventCallback<Character> OnCharacterSave { get; set; }
        #endregion

        /// <summary>
        /// view Model for manipulation by the UI
        /// </summary>
        private ViewCharacter ViewModel { get; set; } = new();

        // a mudblazor hack, since the datetimepicker requires a nullable var
        //private DateTime? date //= DateTime.Now;
        //{
        //    get { return ViewModel.CreatedDate; }
        //    set { ViewModel.CreatedDate = value ?? DateTime.Now; }
        //}

        public bool IsEditable { get; set; } = false;
        private ViewCharacter _backup = null!;

        public void Cancel()
        {
            // ok this is not optimal ...
            //_character.CopyShallow(_backup);
            ViewModel.CopyShallow(_backup);
            //date = _character.CreatedDate;
            IsEditable = false;
        }

        public void Edit()
        {
            _backup = ViewModel;
            IsEditable = true;
        }

        public async Task Save()
        {
            // hmm ef tracks the object after 'update'
            // to 'break' that we need a copy here 
            //Character copy = new();
            //copy.CopyShallow(Model);

            await CharacterService.Update(Model);
            await OnCharacterSave.InvokeAsync(Model);
            IsEditable = false;
        }
    }
}
