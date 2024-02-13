using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class RunnerWriteDbAsync<Tin, Tout>
    {
        private readonly IBizActionAsync<Tin, Tout> _actionClass;
        private readonly DbContext _context;

        public IImmutableList<ValidationResult> Errors => _actionClass.Errors;
        public bool HasErrors => _actionClass.HasErrors;

        public RunnerWriteDbAsync(
            IBizActionAsync<Tin, Tout> actionClass,
            DbContext context)
        {
            _actionClass = actionClass;
            _context = context;
        }

        public async Task<Tout> RunAction(Tin dataIn)
        {
            var result = await _actionClass.ActionAsync(dataIn);
            try
            {
                if (!HasErrors)
                    await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                int x = 1;
            }
            return result;
        }
    }
}
