using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessDataAccess.Utilities;
using System.Reflection.PortableExecutable;
using EVEStandard;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEveMailsAction : IBizActionAsync<AddMailDTO, ICollection<Mail>>
    {
        private readonly MailDbAccess _dbAccess;
        private readonly CharacterDbAccess _charcterDbAcces;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEveMailsAction(MailDbAccess dbAccess, CharacterDbAccess characterDbAccess,
             EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
            _charcterDbAcces = characterDbAccess;
            _esiClient = esiClient;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<ICollection<Mail>> ActionAsync(AddMailDTO dto)
        {
            ICollection<Mail> resultMails = new List<Mail>();

            foreach (var esMail in dto.esMailHeaders)
            {
                // copy the simple stuff
                var newDbMail = new Mail().BasicCopyFrom(esMail);
                // translate the difficult things to the ids of our db
                if (esMail.From != null)
                    newDbMail.From = dto.Characters[esMail.From.Value];
                foreach (var recipient in esMail.Recipients)
                {
                    if (recipient == null) continue;
                    EveMailRecipient? eveMailRecipient = null;
                    switch(recipient.RecipientType)
                    {
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.character:
                            var character = dto.Characters[recipient.RecipientId];
                            eveMailRecipient = new EveMailRecipientCharacter() { Character = character};
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.alliance:
                            var alliance = dto.Alliances[recipient.RecipientId];
                            eveMailRecipient = new EveMailRecipientAlliance() { Alliance = alliance };
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.corporation:
                            var corporation = dto.Corporations[recipient.RecipientId];
                            eveMailRecipient = new EveMailRecipientCorporation() { Corporation = corporation };
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.mailing_list:
                            var maillist = dto.MailLists[recipient.RecipientId];
                            eveMailRecipient = new EveMailRecipientMailList() { MailList = maillist };
                            break;
                    }
                    if (eveMailRecipient != null)
                        newDbMail.Recipients.Add(eveMailRecipient);
                }
                foreach (var label in esMail.Labels)
                {
                    if (label != null)
                        newDbMail.Labels.Add(dto.Labels[label.Value]);
                }

                // fetch the mail content
                if (esMail.MailId!= null) { 
                    var content = await _esiClient.Mail.ReturnMailV1Async(dto.authDTO, esMail.MailId.Value);
                    newDbMail.Content = content.Model.Body;
                    newDbMail.IsRead = content.Model.Read ?? false;
                    newDbMail.Subject = content.Model.Subject;
                    newDbMail.CreatedDate = content.Model.Timestamp ?? DateTime.MinValue;
                    newDbMail.EveId = esMail.MailId.Value;
                }

                await _dbAccess.AddAsync(newDbMail);
                resultMails.Add(newDbMail);
            }
            
            return resultMails;
        }

         
    }
}
