using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels;

namespace EveMailHelper.Web.Shared.EveAccount
{
    public partial class EveAccountDeleteDialog : ComponentBase
    {
        #region injections
        [Inject]
        IAccountManager AccountManager { get; set; } = null!;
        #endregion       

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public DataModels.Security.EveAccount EveAccount { get; set; } = null!;

        [Parameter]
        public EventCallback<DataModels.Security.EveAccount> DialogSaved { get; set; }

        #endregion

        #region GUI Components
        #endregion

        void Cancel() => MudDialog.Cancel();

        private async void Delete()
        {
            _ = EveAccount ?? throw new NullReferenceException("Parameter EveAccount is null");
            await DialogSaved.InvokeAsync(EveAccount);
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
