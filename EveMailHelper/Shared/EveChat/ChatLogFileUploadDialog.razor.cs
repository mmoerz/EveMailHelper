using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Security.Permissions;

namespace EveMailHelper.Shared.EveChat
{
    public partial class ChatLogFileUploadDialog : ComponentBase
    {
        #region injections
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public Character Character { get; set; } = null!;
        [Parameter]
        public EventCallback<Character> DialogSaved { get; set; }

        #endregion

        #region GUI Components
        private ChatLogDragnDropUpload? ChatLogUpload = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        void Cancel() => MudDialog.Cancel();

        private async Task Save()
        {
            _ = ChatLogUpload ?? throw new NullReferenceException("ChatLogUpload Component null");
            await ChatLogUpload.AssignUploadedFiles();
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
