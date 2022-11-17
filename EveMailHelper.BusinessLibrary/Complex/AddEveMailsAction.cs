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
            ICollection<EveMail> mailList = new List<EveMail>();

            foreach (var mail in dto.esMails)
            {
                // copy the simple stuff
                var newDbMail = new EveMail().BasicCopyFrom(mail);
                // translate the difficult things to the ids of our db
                newDbMail.From = _charcterDbAcces.GetByEveId(mail.From);
                foreach (var recipient in mail.Recipients)
                {
                    if (recipient == null) continue;
                    switch(recipient.RecipientType)
                    {
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.character:
                            var character = dto.Characters[recipient.RecipientId];
                            newDbMail.Recipients.Add(new EveMailRecipientCharacter() { Character =  character});
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.alliance:
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.corporation:
                            break;
                        case EVEStandard.Models.MailRecipient.RecipientTypeEnum.mailing_list:
                            break;
                    }
                    
                }
                foreach (var label in mail.Labels)
                {
                    if (label != null)
                        newDbMail.Labels.Add(dto.Labels[label.Value]);
                }
                await _dbAccess.AddAsync(newDbMail);
            }
            
            return mailList;
        }

         
    }
}
