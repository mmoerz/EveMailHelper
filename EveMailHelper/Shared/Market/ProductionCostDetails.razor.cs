using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using MudBlazor;
using static MudBlazor.CategoryTypes;
using System.Linq;
using EveMailHelper.ServiceLayer.Models;
using EveMailHelper.ServiceLayer.Managers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EVEStandard.Models;
using EveMailHelper.DataModels.Market;
using static EveMailHelper.ServiceLayer.Managers.ProductionManager;

namespace EveMailHelper.Web.Shared.Market
{
    public partial class ProductionCostDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        #endregion

        #region parameters
        [Parameter]
        public NormalizedProductionCost? NormalizedProductionCost {  get; set; }
        
        [Parameter] 
        public RenderFragment<NormalizedProductionCost> ProductionCostContent { get; set; } = default!;
        #endregion

        //protected override async Task OnInitializedAsync()
        //{
        //}

        /// <summary>
        /// yeah, i know, however it ate my nerves
        /// maybe i will correct this name, but most likely not
        /// </summary>
        public void Refresh()
        {
            StateHasChanged();
        }
    }
}
