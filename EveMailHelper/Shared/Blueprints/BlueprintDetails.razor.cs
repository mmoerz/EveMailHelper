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

namespace EveMailHelper.Web.Shared.Blueprints
{
    public partial class BlueprintDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        #endregion

        private MudTable<BlueprintComponent> _table = null!;
        private BlueprintComponent _mainProduct = null!;
        private IList<BlueprintComponent> _blueprintComponents = new List<BlueprintComponent>();

        private IndustryBlueprint _blueprint = new();

        #region parameters
        [Parameter]
        public IndustryBlueprint Blueprint
        {
            get
            {
                return _blueprint;
            }
            set 
            {
                _blueprint = value;
            }
        }

        [Parameter]
        public string BlueprintName { get; set; } = "no Blueprint";
        #endregion

        public void Reload()
        {
            _table?.ReloadServerData();
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<BlueprintComponent>> ServerReload(TableState state)
        {
            TableData<BlueprintComponent> data = new();

            if (_blueprint != null && _blueprint.TypeId != 0)
            {
                // TODO: ugly ugly reference to use '11' as an activity filter directly
                _mainProduct = await BlueprintManager.GetBlueprintComponentsList(_blueprint, 11);
                _blueprintComponents = _mainProduct.SubComponents.ToList();
                _blueprintComponents.ToFlatList();
            }

            data.TotalItems = _blueprintComponents.Count;
            data.Items = _blueprintComponents;

            return data;
        }

        private string GetClassForDepth(int depth)
        {
            if (depth == 1)
                return "";
            return string.Format("ml-{0}", (depth-1)*2);
        }

        public void SetModels(List<BlueprintComponent> value)
        {
            _blueprintComponents = value;
        }
    }
}
