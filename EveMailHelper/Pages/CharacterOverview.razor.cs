using EveMailHelper.DataAccessLayer.Models;
using EveMailHelper.Shared;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Pages
{
    public partial class CharacterOverview : ComponentBase
    {
        #region injections
        #endregion

        #region parameters
        #endregion

        public CharacterList? ListOfCharacters = null!;

        public Character Model { get; set; } = new();

        private void CharacterSelected(Character character)
        {
            Model.CopyShallow(character);
        }

        private void CharacterChanged(Character character)
        {
            _ = character ?? throw new ArgumentNullException(nameof(character));

            ListOfCharacters?.Reload();
        }
    }
}
