﻿using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.Web.Shared.Market
{
    public partial class NormalizedProdCostList : ComponentBase
    {
        #region injections
        //[Inject] IBlueprintManager BlueprintManager { get; set; } = null!;

        [Inject] IProductionManager ProductionManager { get; set; } = null!;

        //[Inject] IDialogService DialogService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public EventCallback<NormalizedProductionCost> OnBlueprintSelected { get; set; }
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<NormalizedProductionCost>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private NormalizedProductionCost model = null!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<NormalizedProductionCost>> ServerReload(TableState state)
        {
            TableData<NormalizedProductionCost> onePage =
                await ProductionManager.GetPaginatedNormalizedProductionCostAsync(searchString, state);

            //await Task.Delay(300);

            return onePage;
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        
        private void RowClickEvent(TableRowClickEventArgs<NormalizedProductionCost> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            OnBlueprintSelected.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(NormalizedProductionCost rmodel, int rowNumber)
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
