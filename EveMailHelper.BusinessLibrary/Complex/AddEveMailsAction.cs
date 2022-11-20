using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.BusinessDataAccess.Utilities;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEveMailsAction : IBizActionAsync<AddMailDTO, ICollection<EveMail>>
    {
        readonly EveMailDbAccess _dbAccess;
        readonly CharacterDbAccess _charcterDbAcces;
        private List<ValidationResult> _errors = new();

        public AddEveMailsAction(EveMailDbAccess dbAccess, CharacterDbAccess characterDbAccess)
        {
            _dbAccess = dbAccess;
            _charcterDbAcces = characterDbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<ICollection<EveMail>> ActionAsync(AddMailDTO dto)
        {
            ICollection<EveMail> resultMails = new List<EveMail>();

            foreach (var esMail in dto.esMails)
            {
                // copy the simple stuff
                var newDbMail = new EveMail().BasicCopyFrom(esMail);
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
                await _dbAccess.AddAsync(newDbMail);
            }
            
            return resultMails;
        }

         
    }
}
