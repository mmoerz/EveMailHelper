using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using MudBlazor;

using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.ServiceLayer.Managers;
using EveMailHelper.DataModels;

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
        private MudTable<Mail>? Table { get; set; } = null;
        #endregion

        private string SearchString { get; set; } = string.Empty;
        private int selectedRowNumber = -1;
        //protected override async Task OnInitializedAsync()
        //{
        //}

        private async Task<TableData<Mail>> ServerReload(TableState state)
        {
            return await InGameMailManager.GetInboxMails(SearchString, state);
        }

        private void OnSearch(string text)
        {
            SearchString = text;
            Table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<Mail> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            //OnCharacterSelect.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(Mail rmodel, int rowNumber)
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
