using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels.Sde;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace EveMailHelper.Web.Shared.Blueprints
{
    public partial class BlueprintDetails : Microsoft.AspNetCore.Components.ComponentBase
    {
        #region injections
        [Inject] IBlueprintManager BlueprintManager { get; set; } = null!;
        #endregion

        private IndustryBlueprint _model = null!;

        private MudTable<BlueprintComponent> _table = null!;
        private IEnumerable<BlueprintComponent> _blueprintComponents = null!;

        #region parameters
        [Parameter]
        public IndustryBlueprint Model
        {
            get
            {
                //_model.CopyShallow(ViewModel);
                return _model;
            }
            set 
            {
                _model = value;
                //ViewModel.CopyShallow(value);
            }
        }
        #endregion

        /// <summary>
        /// view Model for manipulation by the UI
        /// </summary>
        //private ViewCharacter ViewModel { get; set; } = new();

        protected void ComposeBlueprintComponents()
        {
            _blueprintComponents = new List<BlueprintComponent>();

            _model.
        }

        protected override async Task OnInitializedAsync()
        {
            //_blueprintComponents = 
        }

        public void SetModel(IndustryBlueprint blueprint)
        {
            Model = blueprint;
        }       

        protected Dictionary<CharacterStatus, string> GetCharacterStati()
        {
            Dictionary<CharacterStatus, string> result = new();
            var stati = Enum.GetValues<CharacterStatus>().ToList();
            foreach(var status in stati)
            {
                result[status] = status.ToString();
            }
            return result;
        }
    }
}
