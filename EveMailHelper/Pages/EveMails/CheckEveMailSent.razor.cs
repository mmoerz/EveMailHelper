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

        private MudTextField<int>? TextFieldAmount = null!;
        private int ReceiverAmount { get; set; }
        private string _receiverString = null!;
        private string ReceiverString
        {
            get
            {
                return _receiverString;
            }
            set 
            {
                _receiverString = value;
                var helpAmount = 0;
                if (_receiverString != null)
                    helpAmount = value.SplitStringOfCharacters(',').Count();
                TextFieldAmount?.SetText(helpAmount.ToString());
            }
        }

        private MudTextField<int>? TextFieldAmountFiltered = null!;
        private int ReceiverFilteredAmount { get; set; }

        private string _filteredReceiverString = null!;
        private string FilteredReceiverString {
            get
            {
                return _filteredReceiverString;
            }
            set
            {
                _filteredReceiverString = value;
                var helpAmount = 0;
                if (_filteredReceiverString != null)
                    helpAmount = value.SplitStringOfCharacters(',').Count();
                TextFieldAmountFiltered?.SetText(helpAmount.ToString());
            }
        }
        private bool SubmitDisabled { get; set; } = false;

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
            if (ReceiverString == null)
                return;

            SubmitDisabled = true;

            var receivers = ReceiverString.SplitStringOfCharacters(',');
            var filterTime = CombineDateAndTime(Date.Value, Time);
            
            var filteredReceivers = await EveMailService.FilterReceivers(ReceiverString, filterTime);
            FilteredReceiverString = string.Join(", ", filteredReceivers);

            SubmitDisabled = false;
        }

        private static DateTime CombineDateAndTime(DateTime? dateTime, TimeSpan? timeSpan)
        {
            _ = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            _ = timeSpan ?? throw new ArgumentNullException(nameof(timeSpan));
            
            return dateTime.Value.Date + timeSpan.Value;
        }
    }
}
