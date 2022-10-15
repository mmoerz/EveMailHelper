using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper
{
    public partial class CharacterList : ComponentBase
    {
        #region injections
        [Inject] ICharacterService CharacterService { get; set; } = null!;

        #endregion

        #region parameters
        #endregion

        #region pagination stuff
        private bool dense = true;
        private bool hover = true;
        private bool readOnly = false;
        private bool fixed_heaer = true;
        private bool fixed_footer = true;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<Character> table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private Character model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<Character>> ServerReload(TableState state)
        {
            TableData<Character> onePage =
                await CharacterService.GetPaginated(searchString, state);

            await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Character> tableRowClickEventArgs)
        {
        }

        private string SelectedRowClassFunc(Character rmodel, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (table.SelectedItem != null && table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;
                model = rmodel;

                return "selected";
            }
            return string.Empty;
        }
    }
}
