using EveMailHelper.BusinessLibrary.Interfaces;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.ChatLogParser;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace EveMailHelper.Shared.Chats
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
        public EventCallback<Chat> OnChatSave { get; set; }

        [Parameter]
        public bool ButtonsEnabled { get; set; } = true;

        [Parameter]
        public bool IsEditable { get; set; } = false;
        #endregion

        // a mudblazor hack, since the datetimepicker requires a nullable var
        private DateTime? Date //= DateTime.Now;
        {
            get { return Chat.SessionStarted; }
            set { Chat.SessionStarted = value ?? DateTime.Now; }
        }
        
        private Chat _backup = null!;

        public void Cancel()
        {
            Chat.CopyShallow(_backup);
            IsEditable = false;
        }

        public void Edit()
        {
            _backup = Chat;
            IsEditable = true;
        }

        public async Task Save()
        {
            try
            {
                await ChatService.UpdateTracked(Chat);
                await OnChatSave.InvokeAsync(Chat);
                IsEditable = false;
            }
            catch
            {
                Snackbar.Add("Failed to Add Note", Severity.Error);
            }
        }
    }
}
