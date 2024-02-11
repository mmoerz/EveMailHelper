using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using EVEStandard.Models;

namespace EveMailHelper.Web.Shared.Market
{
    public partial class MarketOrderList : ComponentBase
    {
        #region injections
        [Inject] IMarketManager MarketManager { get; set; } = null!;

        #endregion

        #region parameters
        [Parameter]
        public int RegionId { get; set; }
        [Parameter]
        public int TypeId { get; set; }

        [Parameter]
        public EventCallback<MarketOrder> OnBlueprintSelected { get; set; }
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        //private IEnumerable<Report> pagedData = null!;
        private MudTable<MarketOrder>? table = null!;
        //private int totalItems;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private MarketOrder model = null!;
        #endregion

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<MarketOrder>> ServerReload(TableState state)
        {
            if (RegionId  == 0)
                return new() { Items = new List<MarketOrder>(), TotalItems = 0 };

            var marketOrders = await MarketManager.LoadMarketPrice(RegionId, TypeId, state.Page);

            TableData<MarketOrder> onePage = new()
            {
                Items = marketOrders,
                TotalItems = marketOrders.Count,
            };

            //await Task.Delay(300);

            return onePage;
        }

        public void Reload()
        {
            table?.ReloadServerData();
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        
        private void RowClickEvent(TableRowClickEventArgs<MarketOrder> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            OnBlueprintSelected.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(MarketOrder rmodel, int rowNumber)
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
