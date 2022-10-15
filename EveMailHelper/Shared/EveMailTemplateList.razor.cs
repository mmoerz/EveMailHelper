using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Shared
{
    public partial class EveMailTemplateList : ComponentBase
    {
        #region injections
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;

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
        private MudTable<EveMailTemplate> table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private EveMailTemplate model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<EveMailTemplate>> ServerReload(TableState state)
        {
            TableData<EveMailTemplate> onePage =
                await EveMailTemplateService.GetPaginated(searchString, state);

            await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<EveMailTemplate> tableRowClickEventArgs)
        {
        }

        private string SelectedRowClassFunc(EveMailTemplate rmodel, int rowNumber)
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
