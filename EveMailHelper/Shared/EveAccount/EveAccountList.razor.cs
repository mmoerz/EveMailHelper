using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.ServiceLayer.Interfaces;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

namespace EveMailHelper.Web.Shared.EveAccount
{
    public partial class EveAccountList : ComponentBase
    {
        #region injections
        [Inject] IDialogService DialogService { get; set; } = null!;
        //[Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] IAccountManager AccountManager { get; set; } = null!;

        #endregion

        #region parameters
        [Parameter]
        public DataModels.Security.Account Account { get; set; } = null!;
        [Parameter]
        public EventCallback<DataModels.Security.EveAccount> OnEveAccountSelect { get; set; }
        #endregion

        #region pagination stuff
        private readonly bool readOnly = false;

        private MudTable<DataModels.Security.EveAccount>? table = null!;
        private string searchString = "";
        #endregion

        #region rowselection
        private int selectedRowNumber = -1;
        //private Character model = null!;
        #endregion

        public void Reload()
        {
            table?.ReloadServerData();
        }

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<DataModels.Security.EveAccount>> ServerReload(TableState state)
        {
            return await AccountManager.GetEveAccountsPaginated(Account, searchString, state);
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table?.ReloadServerData();
        }

        private void RowClickEvent(TableRowClickEventArgs<DataModels.Security.EveAccount> tableRowClickEventArgs)
        {
            if (tableRowClickEventArgs == null)
                return;
            OnEveAccountSelect.InvokeAsync(tableRowClickEventArgs.Item);
        }

        private string SelectedRowClassFunc(DataModels.Security.EveAccount rmodel, int rowNumber)
        {
            if (selectedRowNumber == rowNumber)
            {
                selectedRowNumber = -1;
                return string.Empty;
            }
            else if (table?.SelectedItem != null && table.SelectedItem.Equals(rmodel))
            {
                selectedRowNumber = rowNumber;
                //model = rmodel;

                return "selected";
            }
            return string.Empty;
        }

        private void DeleteCharacter(DataModels.Security.EveAccount character)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                { "Character", character },
                { "DialogSaved", new EventCallback<DataModels.Security.EveAccount>(
                    this, new Action<DataModels.Security.EveAccount>(FinallyDeleteCharacter)) }
            };
            DialogService.Show<EveAccountDeleteDialog>("Delete EveAccount", parameters, options);
        }

        private void FinallyDeleteCharacter(DataModels.Security.EveAccount eveAccount)
        {
            AccountManager.Remove(eveAccount);
            table?.ReloadServerData();
        }

        
    }
}
