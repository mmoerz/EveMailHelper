using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels;

namespace EveMailHelper.Web.Shared.EveChar
{
    public partial class CharacterDeleteDialog : ComponentBase
    {
        #region injections
        [Inject]
        ICharacterService CharacterService { get; set; } = null!;
        #endregion       

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public Character Character { get; set; } = null!;

        [Parameter]
        public EventCallback<Character> DialogSaved { get; set; }

        #endregion

        #region GUI Components
        #endregion

        void Cancel() => MudDialog.Cancel();

        private async void Delete()
        {
            _ = Character ?? throw new NullReferenceException("Parameter Character is null");
            await DialogSaved.InvokeAsync(Character);
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
