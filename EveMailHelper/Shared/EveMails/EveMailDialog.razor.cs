﻿using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace EveMailHelper.Shared.EveMails
{
    public partial class EveMailDialog : ComponentBase
    {
        #region injections
        [Inject] IEveMailService EveMailService { get; set; } = null!;
        #endregion

        #region parameters
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; } = null!;
        [Parameter]
        public EveMail Model { get; set; } = new();
        [Parameter]
        public EventCallback<EveMail> DialogSaved { get; set; }
        #endregion

        MudForm? form = null!;

        private async Task Save()
        {
            //await form.Validate();

            //if (form.IsValid)
            {
                // Notify parent component to
                // submit the changed Analysisrequest
                EveMailService.Update(Model);
                await DialogSaved.InvokeAsync(Model);
            }

            MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

    }
}