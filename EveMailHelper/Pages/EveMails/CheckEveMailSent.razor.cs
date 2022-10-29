using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessLibrary.Services;
using EveMailHelper.BusinessLibrary.Tools;
using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.Pages.EveMails
{
    public partial class CheckEveMailSent : ComponentBase
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
        private string FilteredReceiverString { get; set; } = null!;
        private Guid TemplateId { get; set; }
        private ICollection<EveMailTemplate> templates = null!;

        /// <summary>
        /// initialized with yesterday
        /// </summary>
        private DateTime? Date { get; set; } = DateTime.Now.AddDays(-1);
        private TimeSpan? Time { get; set; } = TimeSpan.FromMinutes(DateTime.Now.Hour * 60 + DateTime.Now.Minute);

        //protected override async Task OnInitializedAsync()
        //{
        //    //dbContext = await dbContextFactory.CreateDbContextAsync();
            
        //}

        private static void Cancel()
        {
            //newModels();
        }

        private async Task Submit()
        {
            if (Date == null)
                return;
            var receivers = ReceiverString.SplitStringOfCharacters(',');
            var filterTime = CombineDateAndTime(Date.Value, Time);
            
            var filteredReceivers = await EveMailService.FilterReceivers(ReceiverString, filterTime);
            FilteredReceiverString = string.Join(", ", filteredReceivers);
        }

        private static DateTime CombineDateAndTime(DateTime? dateTime, TimeSpan? timeSpan)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _ = timeSpan ?? throw new ArgumentNullException(nameof(timeSpan));
            
            return dateTime.Value.Date + timeSpan.Value;
        }
    }
}
