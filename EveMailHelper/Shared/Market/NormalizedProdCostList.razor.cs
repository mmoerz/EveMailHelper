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
        [Inject] IMapManager MapManager { get; set; } = null!;

        [Inject] IProductionManager ProductionManager { get; set; } = null!;

        //[Inject] IDialogService DialogService { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public EventCallback<NormalizedProductionCost> OnBlueprintSelected { get; set; }
        [Parameter]
        public int RegionId { get; set; }
        [Parameter]
        public double SystemCostIndex { get; set; }
        [Parameter]
        public double StructureBonuses { get; set; }
        [Parameter]
        public double FacilityTax { get; set; }
        [Parameter]
        public double MaterialConsumptionModifier { get; set; }
        #endregion

        #region pagination stuff

        private MudTable<NormalizedProductionCost>? table = null!;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        private NormalizedProductionCost model = null!;
        #endregion

        bool initfinished = false;

        protected int PreprocessItemNr = 0;
        protected int PreprocessItemMax = 0;
        protected int PreprocessProgress = 0;

        protected bool Loading = true;

        protected override async Task OnInitializedAsync()
        {
            var region = await MapManager.GetRegionByName("The Forge");
            _ = region ?? throw new Exception("Jita region not found");
            RegionId = region.EveId;

            await ProductionManager.PreprocessBlueprintsForActivity(
                new Progress<Tuple<int, int>>((tuple) =>
                {
                    PreprocessItemNr = tuple.Item1;
                    PreprocessItemMax = tuple.Item2;
                    PreprocessProgress = PreprocessItemNr * 100 / PreprocessItemMax;
                    if (PreprocessProgress == 100)
                    {
                        Loading = false;
                    }
                    StateHasChanged();
                }),
                11, 20, 60,
                RegionId, SystemCostIndex, StructureBonuses, FacilityTax, MaterialConsumptionModifier, false); ;
            //initfinished = true;

            //table?.ReloadServerData();
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<NormalizedProductionCost>> ServerReload(TableState state)
        {

            TableData<NormalizedProductionCost> onePage;
            onePage = await ProductionManager.GetPaginatedNormalizedProductionCostAsync(searchString, state);

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
            if (table?.SelectedItem != null && table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;
                model = rmodel;

                return "selected";
            }
            return string.Empty;
        }

        public double GetBestPriceWinningsInPercent(NormalizedProductionCost cost)
        {
            double onepercent = cost.BestPriceSum / 100;
            return (cost.ProductCostSum - cost.BestPriceSum) / onepercent;
        }

        public double GetDirectPriceWinningsInPercent(NormalizedProductionCost cost)
        {
            double onepercent = cost.DirectCostSum / 100;
            return (cost.ProductCostSum - cost.DirectCostSum) / onepercent;
        }
    }
}
