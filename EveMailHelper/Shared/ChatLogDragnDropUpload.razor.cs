
using EveMailHelper.DataModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace EveMailHelper.Shared
{
    public partial class ChatLogDragnDropUpload : ComponentBase
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

        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new();

        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add("TODO: Upload your files!", Severity.Normal);

            if (Character == null || Character.Id == Guid.Empty)
            {
                Snackbar.Add($"No character selected as a receiver of the chats.");
                return;
            }

            var files = e.GetMultipleFiles();
            foreach (var file in files)
            {
                fileNames.Add(file.Name);
                using Stream stream = file.OpenReadStream(MaxFileSize); //read file with size in kb                    
                using FileStream fs = File.Create(file.Name);
                await stream.CopyToAsync(fs);
                Snackbar.Add($"finished uploading {file.Name}");
            }

            Snackbar.Add("Fileuploads finished.");
        }

        private async Task Clear()
        {
            Clearing = true;
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;
        }
        //private async Task Upload()
        //{
        //    //Upload the files here
        //    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        //    Snackbar.Add("TODO: Upload your files!", Severity.Normal);

        //    if (Character == null)
        //    {
        //        Snackbar.Add($"No character selected as a receiver of the chats.");
        //        return;
        //    }

        //    //foreach (var file in browserFiles)
        //    //{
        //    //    using Stream stream = file.OpenReadStream(MaxFileSize); //read file with size in kb                    
        //    //    using FileStream fs = File.Create(file.Name);
        //    //    await stream.CopyToAsync(fs);
        //    //    Snackbar.Add($"finished uploading {file.Name}");
        //    //}

        //    Snackbar.Add("Fileuploads finished.");
        //}

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            

            

            var entries = e.GetMultipleFiles();
            
            //Snackbar.Add($"Files with {entries.FirstOrDefault().Size} bytes size are not allowed", Severity.Error);
            //Snackbar.Add($"Files starting with letter {entries.FirstOrDefault().Name.Substring(0, 1)} are not recommended", Severity.Warning);
            Snackbar.Add($"This file has the extension {entries.FirstOrDefault().Name.Split(".").Last()}", Severity.Info);

            //TODO upload the files to the server

            
        }
    }
}
