using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class SendEveMailBasedOnTemplateAction : IBizActionAsync<SendTemplateToDTO, Mail>
    {
        readonly MailDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public SendEveMailBasedOnTemplateAction(MailDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<Mail> ActionAsync(SendTemplateToDTO dto)
        {
            dto.Template.CreateMail(out Mail eveMail, dto.FromCharacter);
            foreach (var receiver in dto.Characters)
            {
                eveMail.SentTo.Add(new EveMailSentTo() { Character = receiver });
            }
            await _dbAccess.AddAsync(eveMail);
            return eveMail;
        }
    }
}
