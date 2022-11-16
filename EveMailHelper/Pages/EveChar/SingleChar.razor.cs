using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.Web.Shared;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Web.Pages.EveChar
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
                Character = CharacterService.GetById(Guid.Parse(value)).Result;
            } 
        }
        
        #endregion

        #region GUI Components
        #endregion
        public CharacterCommunicationList? CommunicationList = null!;

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
