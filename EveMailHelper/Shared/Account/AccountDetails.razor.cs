using Microsoft.AspNetCore.Components;

using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;
using EveMailHelper.Web.Models;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Web.Shared.Account
{
    public partial class AccountDetails : ComponentBase
    {
        #region injections
        [Inject] IAccountManager AccountManager { get; set; } = null!;
        #endregion

        private DataModels.Security.Account _backup = new();

        #region parameters
        [Parameter]
        public DataModels.Security.Account Model { get; set; } = null!;
        
        [Parameter]
        public EventCallback<DataModels.Security.Account> OnAccountSave { get; set; }
        #endregion

        // Mudblazor workaround
        public DateTime? Created { 
            get
            { 
                return Model.CreatedDate; 
            }
            set
            {
                Model.CreatedDate = value.GetValueOrDefault(DateTime.MinValue);
            }
        }
        
        public bool IsEditable { get; set; } = false;
        
        public void Cancel()
        {
            // ok this is not optimal ...
            Model.CopyShallowNoId(_backup);
            Created = _backup.CreatedDate;
            IsEditable = false;
        }

        public void Edit()
        {
            _backup.CopyShallowNoId(Model);
            IsEditable = true;
        }

        public async Task Save()
        {
            // hmm ef tracks the object after 'update'
            // to 'break' that we need a copy here 
            //Character copy = new();
            //copy.CopyShallow(Model);

            await AccountManager.Update(Model);
            await OnAccountSave.InvokeAsync(Model);
            IsEditable = false;
        }
    }
}
