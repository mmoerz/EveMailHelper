using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataModels;
using EveMailHelper.Shared.EveChat;
using EveMailHelper.Shared.EveMails;
using EveMailHelper.Shared.Notes;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Shared
{
    public partial class CharacterCommunicationList : ComponentBase
    {
        #region injections
        //[Inject] ICharacterService CharacterService { get; set; } = null!;
        [Inject] ICommunicationService CommunicationService { get; set; } = null!;
        [Inject] IDialogService DialogService { get; set; } = null!;
        #endregion

        #region parameters
        //[Parameter]
        //public EventCallback<Character> OnCharacterSelect { get; set; }
        [Parameter]
        public Character Character { get; set; } = null!;

        #endregion

        #region pagination stuff

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<Communication>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        //private Communication model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        public void Reload()
        {
            table?.ReloadServerData();
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<Communication>> ServerReload(TableState state)
        {
            TableData<Communication> onePage =
                await CommunicationService.GetPaginated(Character, searchString, state);

            //await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Communication> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            if (tableRowClickEventArgs.Item.obj.GetType() == typeof(Chat))
            {
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var parameters = new DialogParameters
                    {
                    { "model", tableRowClickEventArgs.Item.obj },
                    { "DialogSaved", new EventCallback<Character>(this, new Action<Character>(ChatDialogWasSaved)) }
                    };
                //var dialog = DialogService.Show<ChatDialog>("Edit Chat", parameters, options);
            }
            if (tableRowClickEventArgs.Item.obj.GetType() == typeof(EveMail))
            {
                var dialog = CreateEmailDialog(tableRowClickEventArgs.Item.obj);
            }
            if (tableRowClickEventArgs.Item.obj.GetType() == typeof(Note))
            {
                var dialog = CreateNoteDialog(tableRowClickEventArgs.Item.obj);
            }
        }

        private string SelectedRowClassFunc(Communication rmodel, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (table?.SelectedItem != null && table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;



                //model = rmodel;

                return "selected";
            }
            return string.Empty;
        }

        private IDialogReference CreateChatDialog(object chat)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
                {
                    { "Character", Character },
                    { "DialogSaved", new EventCallback<Character>(this, new Action<Character>(ChatDialogWasSaved)) }
                };
            return DialogService.Show<ChatLogFileUploadDialog>("Add Chats", parameters, options);
        }

        private IDialogReference CreateEmailDialog(object email)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
                {
                    { "model", email },
                    { "DialogSaved", new EventCallback<EveMail>(this, new Action<EveMail>(EveMailDialogWasSaved)) }
                };
            return DialogService.Show<EveMailDialog>("Edit Eve Mail", parameters, options);
        }

        private IDialogReference CreateNoteDialog(object note)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
                {
                    { "model", note },
                    { "DialogSaved", new EventCallback<Note>(this, new Action<Note>(NoteDialogWasSaved)) }
                };
            return DialogService.Show<NoteDialog>("Edit Note", parameters, options);
        }

        private void AddEveMail()
        {
            var dialog = CreateEmailDialog(new EveMail());
        }

        private void AddNote()
        {
            var dialog = CreateNoteDialog(new Note() { AttachedTo = Character });
        }

        private void AddChat()
        {
            var dialog = CreateChatDialog(new Chat() { AttachedTo = Character });
        }

        private void EveMailDialogWasSaved(EveMail eveMail)
        {
            table?.ReloadServerData();
        }
        private void NoteDialogWasSaved(Note note)
        {
            table?.ReloadServerData();
        }

        private void ChatDialogWasSaved(Character character)
        {
            table?.ReloadServerData();
        }
    }
}
