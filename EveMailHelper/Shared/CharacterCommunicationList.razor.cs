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
        #endregion

        #region parameters
        //[Parameter]
        //public EventCallback<Character> OnCharacterSelect { get; set; }
        [Parameter]
        public Character Character { get; set; } = null!;

        #endregion

        #region pagination stuff

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<Communication> table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private Communication model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        public void Reload()
        {
            table.ReloadServerData();
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
            table.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Communication> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
        }

        private string SelectedRowClassFunc(Communication rmodel, int rowNumber)
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
