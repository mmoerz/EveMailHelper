using EveMailHelper.DataModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace EveMailHelper.Shared
{
    public partial class ChatLogUpload : ComponentBase
    {
        #region injections
        [Inject] ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public Character Character { get; set; } = null!;

        #endregion

        #region GUI Components
        #endregion

        //protected override async Task OnInitializedAsync()
        //{

        //}

        private int MaxFileSize = 1024000;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            //Do your validations here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;

            if (Character == null)
            {
                Snackbar.Add($"No character selected as a receiver of the chats.");
                return;
            }

            var entries = e.GetMultipleFiles();
            
            //Snackbar.Add($"Files with {entries.FirstOrDefault().Size} bytes size are not allowed", Severity.Error);
            //Snackbar.Add($"Files starting with letter {entries.FirstOrDefault().Name.Substring(0, 1)} are not recommended", Severity.Warning);
            Snackbar.Add($"This file has the extension {entries.FirstOrDefault().Name.Split(".").Last()}", Severity.Info);

            //TODO upload the files to the server

            var files = e.GetMultipleFiles();
            foreach (var file in files)
            {
                using Stream stream = file.OpenReadStream(MaxFileSize); //read file with size in kb                    
                using FileStream fs = File.Create(file.Name);
                await stream.CopyToAsync(fs);
                Snackbar.Add($"finished uploading {file.Name}");
            }

            Snackbar.Add("Fileuploads finished.");
        }
    }
}
