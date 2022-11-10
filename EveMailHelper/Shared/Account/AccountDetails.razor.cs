using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Web.Shared.Account
{
    public partial class AccountDetails : ComponentBase
    {
        #region injections
        [Inject] ICharacterService CharacterService { get; set; } = null!;
        #endregion

        private Character _model = null!;

        #region parameters
        [Parameter]
        public DataModels.Security.Account Model { get; set; } = null!;
        
        [Parameter]
        public EventCallback<Character> OnCharacterSave { get; set; }
        #endregion

        // Mudblazor workaround
        public DateTime? Created { 
            get
            { 
                return Model.CreatedDate; 
            }
            set
            {
                Model.CreatedDate = value.GetValueOrDefault(DateTime.MinValue);
            }
        }
        
        public bool IsEditable { get; set; } = false;
        
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
