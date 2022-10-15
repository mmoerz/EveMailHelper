using FluentValidation.Results;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public interface IBizActionAsync<in Tin, Tout>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        public Task<Tout> ActionAsync(Tin dto);
    }
}
