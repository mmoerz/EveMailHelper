using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

namespace EveMailHelper.Shared
{
    public partial class CharacterDetails : ComponentBase
    {
        #region injections
        #endregion

        #region parameters
        [Parameter]
        public Character Model { get; set; } = null!;
        #endregion

        DateTime? date = DateTime.Now;
    }
}
