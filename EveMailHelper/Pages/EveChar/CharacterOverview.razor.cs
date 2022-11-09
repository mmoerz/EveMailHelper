using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Shared.EveChar;

namespace EveMailHelper.Web.Pages.EveChar
{
    public partial class CharacterOverview : ComponentBase
    {
        #region injections
        #endregion

        #region parameters
        #endregion

        public CharacterList? ListOfCharacters = null!;

        //public Character Model { get; set; } = new();

        private CharacterDetails? CharDetails { get; set; } = null!;

        private void CharacterSelected(Character character)
        {
            _ = CharDetails ?? throw new NullReferenceException("CharDetails");
            CharDetails.SetModel(character);
        }

        private void CharacterChanged(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            ListOfCharacters?.Reload();
        }
    }
}
