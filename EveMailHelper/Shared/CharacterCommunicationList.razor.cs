using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

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
                    { "DialogSaved", new EventCallback<Chat>(this, new Action<Chat>(ChatDialogWasSaved)) }
                    };
                var dialog = DialogService.Show<EveMailDialog>("Edit Chat", parameters, options);
            }
            if (tableRowClickEventArgs.Item.obj.GetType() == typeof(EveMail))
            {
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var parameters = new DialogParameters
                {
                    { "model", tableRowClickEventArgs.Item.obj },
                    { "DialogSaved", new EventCallback<EveMail>(this, new Action<EveMail>(EveMailDialogWasSaved)) }
                };
                var dialog = DialogService.Show<EveMailDialog>("Edit Eve Mail", parameters, options);
            }
            if (tableRowClickEventArgs.Item.obj.GetType() == typeof(Note))
            {
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var parameters = new DialogParameters
                {
                    { "model", tableRowClickEventArgs.Item.obj },
                    { "DialogSaved", new EventCallback<Note>(this, new Action<Note>(NoteDialogWasSaved)) }
                };
                var dialog = DialogService.Show<EveMailDialog>("Edit Eve Mail", parameters, options);
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

        private void EveMailDialogWasSaved(EveMail eveMail)
        {
            table?.ReloadServerData();
        }
        private void NoteDialogWasSaved(Note note)
        {
            table?.ReloadServerData();
        }

        private void ChatDialogWasSaved(Chat chat)
        {
            table?.ReloadServerData();
        }
    }
}
