using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Models;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Shared.EveChar
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
        
        [Parameter]
        public EventCallback<Character> OnCharacterSave { get; set; }
        #endregion

        /// <summary>
        /// view Model for manipulation by the UI
        /// </summary>
        private ViewCharacter ViewModel { get; set; } = new();

        public void SetModel(Character character)
        {
            Model = character;
        }

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

        protected Dictionary<CharacterStatus, string> GetCharacterStati()
        {
            Dictionary<CharacterStatus, string> result = new();
            var stati = Enum.GetValues<CharacterStatus>().ToList();
            foreach(var status in stati)
            {
                result[status] = status.ToString();
            }
            return result;
        }
    }
}
