using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Managers;

namespace EveMailHelper.Web.Shared.Reactions
{
    public partial class ReactionList : ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;

        //[Inject] IDialogService DialogService { get; set; } = null!;
        #endregion

        #region parameters
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<IndustryBlueprint>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private IndustryBlueprint model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<IndustryBlueprint>> ServerReload(TableState state)
        {
            TableData<IndustryBlueprint> onePage =
                await BlueprintManager.GetBlueprintsPaginated("Formulas", searchString, state);

            //await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        
        private void RowClickEvent(TableRowClickEventArgs<IndustryBlueprint> tableRowClickEventArgs)
        {
            if (selectedRowNumber != -1)
            {
                /*
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var parameters = new DialogParameters
                {
                    { "model", tableRowClickEventArgs.Item },
                    { "DialogSaved", new EventCallback<Mail>(this, new Action<Mail>(DialogWasSaved)) }
                };
                var dialog = DialogService.Show<EveMailDialog>("Edit Eve Mail", parameters, options);
                */
            }
        }

        private string SelectedRowClassFunc(IndustryBlueprint rmodel, int rowNumber)
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
        
    }
}
