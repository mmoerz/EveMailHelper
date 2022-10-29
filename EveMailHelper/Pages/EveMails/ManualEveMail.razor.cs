﻿using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.BusinessLibrary.Tools;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Pages.EveMails
{
    public partial class ManualEveMail : ComponentBase
    {
        #region injections
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;
        // maybe of use when coloring the already known chars
        //[Inject] ICharacterService CharacterService { get; set; } = null!;
        [Inject] IEveMailService EveMailService { get; set; } = null!;

        #endregion

        #region parameters
        #endregion

        #region GUI Components

        #endregion
        private string ReceiverString { get; set; } = null!;
        private Guid TemplateId { get; set; }
        private ICollection<EveMailTemplate> templates = null!;

        protected override async Task OnInitializedAsync()
        {
            //dbContext = await dbContextFactory.CreateDbContextAsync();
            templates = await EveMailTemplateService.GetAll();
        }

        private static void Cancel()
        {
            //newModels();
        }

        private async Task Submit()
        {
            var receivers = ReceiverString.SplitStringOfCharacters(',');
            await EveMailService.SendTo(TemplateId, receivers);
        }
    }
}