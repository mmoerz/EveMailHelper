using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Shared.EveChat
{
    public partial class ChatDetails : ComponentBase
    {
        #region injections
        [Inject] ISnackbar Snackbar { get; set; } = null!;
        [Inject] IChatService ChatService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter] 
        public Chat Chat { get; set; } = null!;

        [Parameter]
        public bool ButtonsEnabled { get; set; } = true;

        #endregion

        public void Close()
        {
        }
    }
}
