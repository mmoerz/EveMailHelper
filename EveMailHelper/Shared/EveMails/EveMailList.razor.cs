using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;
using EveMailHelper.Shared.EveMails;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Shared.EveMails
{
    public partial class EveMailList : ComponentBase
    {
        #region injections
        [Inject] IEveMailService EveMailService { get; set; } = null!;

        [Inject] IDialogService DialogService { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<EveMail>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private EveMail model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<EveMail>> ServerReload(TableState state)
        {
            TableData<EveMail> onePage =
                await EveMailService.GetPaginated(searchString, state);

            //await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<EveMail> tableRowClickEventArgs)
        {
            if (selectedRowNumber != -1)
            {
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var parameters = new DialogParameters
                {
                    { "model", tableRowClickEventArgs.Item },
                    { "DialogSaved", new EventCallback<EveMail>(this, new Action<EveMail>(DialogWasSaved)) }
                };
                var dialog = DialogService.Show<EveMailDialog>("Edit Eve Mail", parameters, options);
            }
        }

        private string SelectedRowClassFunc(EveMail rmodel, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (table?.SelectedItem != null && table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;
                model = rmodel;

                return "selected";
            }
            return string.Empty;
        }

        private void AddNew()
        {
            model = new();

            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth=MaxWidth.Large, FullWidth = true };
            var parameters = new DialogParameters
            {
                { "model", model },
                { "DialogSaved", new EventCallback<EveMail>(this, new Action<EveMail>(DialogWasSaved)) }
            };
            var dialog = DialogService.Show<EveMailDialog>("Edit Eve Mail Template", parameters, options);
        }

        private void DialogWasSaved(EveMail eveMailTemplate)
        {
            table?.ReloadServerData();
        }

        private void DeleteEmail(EveMail mail)
        {
            EveMailService.Delete(mail);
            table?.ReloadServerData();
        }
    }
}
