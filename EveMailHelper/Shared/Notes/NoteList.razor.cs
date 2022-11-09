using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Shared.Notes
{
    public partial class NoteList : ComponentBase
    {
        #region injections
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] INoteService NoteService { get; set; } = null!;

        #endregion

        #region parameters
        [Parameter]
        public EventCallback<Note> OnNoteSelect { get; set; }
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;
        
        //private IEnumerable<Report> pagedData = null!;
        private MudTable<Note>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private Note model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        public void Reload()
        {
            table?.ReloadServerData();
        }

        /// <summary>
        /// fetch the data from the database
        /// </summary>
        private async Task<TableData<Note>> ServerReload(TableState state)
        {
            TableData<Note> onePage =
                await NoteService.GetPaginated(searchString, state);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Note> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            OnNoteSelect.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(Note selectedNote, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (table?.SelectedItem != null && table.SelectedItem.Equals(selectedNote))
            {
                selectedRowNumber = rowNumber;
                model = selectedNote;

                return "selected";
            }
            return string.Empty;
        }

        private async Task DeleteNote(Note note)
        {
            await NoteService.Delete(note);
            table?.ReloadServerData();
        }

        private void NavigateToCharacter(Character character)
        {
            Navigation.NavigateTo($"/EveChar/SingleChar/{character.Id}");
        }
    }
}
