using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;
using EveMailHelper.Shared;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace EveMailHelper.Pages.EveChar
{
    public partial class SingleChar : ComponentBase
    {
        #region injections
        [Inject] ICharacterService CharacterService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public Character Character { get; set; } = new Character();
        [Parameter]
        public string CharacterId {
            get { return Character.Id.ToString(); } 
            set
            {
                Character = CharacterService.GetCharactersById(Guid.Parse(value));
            } 
        }
        
        #endregion

        #region GUI Components
        #endregion
        public CharacterCommunicationList CommunicationList = null!;

        //protected override async Task OnInitializedAsync()
        //{
        //}

        private void CharacterChanged(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            //CommunicationList.Reload();
        }
    }
}
