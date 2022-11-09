using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class SendEveMailBasedOnTemplateAction : IBizActionAsync<SendTemplateToDto, EveMail>
    {
        readonly EveMailDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public SendEveMailBasedOnTemplateAction(EveMailDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<EveMail> ActionAsync(SendTemplateToDto dto)
        {
            dto.Template.CreateMail(out EveMail eveMail);
            foreach (var receiver in dto.Characters)
            {
                eveMail.SentTo.Add(new EveMailSentTo() { Character = receiver });
            }
            await _dbAccess.AddAsync(eveMail);
            return eveMail;
        }
    }
}
