using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Pages
{
    public partial class CharacterOverview : ComponentBase
    {
        #region injections
        #endregion

        #region parameters
        #endregion

        public Character Model { get; set; } = new();

        private void CharacterSelected(Character character)
        {
            Model.CopyShallow(character);
        }

        private void CharacterChanged(Character character)
        {

        }
    }
}
