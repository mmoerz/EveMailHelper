using EveMailHelper.DataAccessLayer.Context;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Pages
{
    public partial class ManualEveMail : ComponentBase
    {
        #region injections
        [Inject] IDbContextFactory<EveMailHelperContext> dbContextFactory { get; set; } = null!;

        #endregion
        private EveMailHelperContext dbContext = null!;

        #region parameters
        #endregion

        #region GUI Components
        private MudForm form = null!;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            dbContext = await dbContextFactory.CreateDbContextAsync();
        }
    }
}
