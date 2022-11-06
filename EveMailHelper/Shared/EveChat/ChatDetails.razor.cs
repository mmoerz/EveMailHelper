using EveMailHelper.BusinessLibrary.Interfaces;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.ChatLogParser;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

using MudBlazor;

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
