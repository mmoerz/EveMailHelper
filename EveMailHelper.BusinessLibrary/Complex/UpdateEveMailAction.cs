using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class UpdateEveMailAction : IBizAction<EveMail, EveMail>
    {
        readonly EveMailDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public UpdateEveMailAction(EveMailDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public EveMail Action(EveMail dto)
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));

            _dbAccess.Update(dto);

            return dto;
        }
    }
}
