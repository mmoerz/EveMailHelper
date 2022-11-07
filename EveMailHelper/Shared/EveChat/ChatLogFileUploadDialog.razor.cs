using Microsoft.AspNetCore.Components;

using MudBlazor;

using System.Security.Permissions;

namespace EveMailHelper.Shared.EveChat
{
    public partial class ChatLogFileUploadDialog : ComponentBase
    {
        #region injections
        //[Inject] IDbContextFactory<Context> dbContextFactory { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        #region GUI Components
        private ChatLogDragnDropUpload? ChatLogUpload = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        private void Cancel()
        {

        }

        private async Task Save()
        {
            _ = ChatLogUpload ?? throw new NullReferenceException("ChatLogUpload Component null");
            await ChatLogUpload.AssignUploadedFiles();
        }
    }
}
