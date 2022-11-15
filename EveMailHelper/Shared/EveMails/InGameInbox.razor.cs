using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Managers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

namespace EveMailHelper.Web.Shared.EveMails
{
    public partial class InGameInbox : ComponentBase
    {
        #region injections
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        [Inject] IAuthenticationManager AuthenticationManager { get; set; } = null!;

        [Inject] IInGameMailManager InGameMailManager { get; set; } = null!;
        #endregion

        #region parameters
        [Parameter]
        public bool ReadOnly { get; set; } = false;
        #endregion

        #region GUI Components
        private MudTable<EVEStandard.Models.Mail>? Table { get; set; } = null;
        #endregion

        private string searchString { get; set; } = string.Empty;
        private int selectedRowNumber = -1;
        //protected override async Task OnInitializedAsync()
        //{
        //}

        private async Task<TableData<EVEStandard.Models.Mail>> ServerReload(TableState state)
        {
            return await InGameMailManager.GetInboxMails(searchString, state);
        }

        private void OnSearch(string text)
        {
            searchString = text;
            Table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<EVEStandard.Models.Mail> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            //OnCharacterSelect.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(EVEStandard.Models.Mail rmodel, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (Table?.SelectedItem != null && Table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;
                //model = rmodel;

                return "selected";
            }
            return string.Empty;
        }
    }
}
