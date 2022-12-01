using Microsoft.AspNetCore.Components;

using MudBlazor;

using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.Web.Pages.EveMails
{
    public partial class MailRecruitment : ComponentBase
    {
        #region injections
        [Inject] IEveMailTemplateService EveMailTemplateService { get; set; } = null!;
        // maybe of use when coloring the already known chars
        //[Inject] ICharacterService CharacterService { get; set; } = null!;
        [Inject] IMailManager MailService { get; set; } = null!;
        [Inject] IInGameCharacterManager InGameCharacterManager { get; set; } = null!;
        [Inject] IInGameMailManager InGameMailManager { get; set; } = null!;

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
            
            var filteredReceivers = await MailService.FilterReceivers(ReceiverString, filterTime);
            var FilteredReceiverString = string.Join(", ", filteredReceivers);

            // load all new characters from eve
            var characters = await InGameCharacterManager.LoadCharactersByName(filteredReceivers);
            
            // check that those characters are all newbies
            // == only starter corporation


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
