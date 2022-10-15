using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class RunnerWriteDb<Tin, Tout>
    {
        private readonly IBizAction<Tin, Tout> _actionClass;
        private readonly DbContext _context;

        public IImmutableList<ValidationResult> Errors => _actionClass.Errors;
        public bool HasErrors => _actionClass.HasErrors;

        public RunnerWriteDb(
            IBizAction<Tin, Tout> actionClass,
            DbContext context)
        {
            _actionClass = actionClass;
            _context = context;
        }

        public Tout RunAction(Tin dataIn)
        {
            var result = _actionClass.Action(dataIn);
            if (!HasErrors)
                _context.SaveChanges();
            return result;
        }
    }
}
