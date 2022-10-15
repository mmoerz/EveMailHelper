using FluentValidation.Results;
using System.Collections.Immutable;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public abstract class BizActionErrors
    {
        private readonly List<ValidationResult> _errors = new();

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();
        public bool HasErrors => _errors.Any();
        protected void AddError(string errorMessage, params string[] propertyNames)
        {
            List<ValidationFailure> failures = new();
            foreach(var property in propertyNames)
                failures.Add(new ValidationFailure(errorMessage, property));
            _errors.Add(new ValidationResult(failures));
        }
    }
}
