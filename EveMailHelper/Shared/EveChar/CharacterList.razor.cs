using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Shared.EveChar
{
    public partial class CharacterList : ComponentBase
    {
        #region injections
        [Inject] IDialogService DialogService { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] ICharacterService CharacterService { get; set; } = null!;

        #endregion

        #region parameters
        [Parameter]
        public EventCallback<Character> OnCharacterSelect { get; set; }
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<Character>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        //private Character model = null!;
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
        private async Task<TableData<Character>> ServerReload(TableState state)
        {
            TableData<Character> onePage =
                await CharacterService.GetPaginated(searchString, state);

            //await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Character> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            OnCharacterSelect.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(Character rmodel, int rowNumber)
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

        private void DeleteCharacter(Character character)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Character", character },
                { "DialogSaved", new EventCallback<Character>(this, new Action<Character>(FinallyDeleteCharacter)) }
            };
            DialogService.Show<CharacterDeleteDialog>("Delete Character", parameters, options);
        }

        private void FinallyDeleteCharacter(Character character)
        {
            CharacterService.Delete(character);
            table?.ReloadServerData();
        }

        private void NavigateToCharacter(Character character)
        {
            Navigation.NavigateTo($"/EveChar/SingleChar/{character.Id}");
        }
    }
}
