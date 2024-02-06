using FluentValidation.Results;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public interface IBizAction<out Tout>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        public Tout Action();
    }

    public interface IBizAction<in Tin, out Tout>
    {
        IImmutableList<ValidationResult> Errors { get; }
        bool HasErrors { get; }
        public Tout Action(Tin dto);
    }
}
