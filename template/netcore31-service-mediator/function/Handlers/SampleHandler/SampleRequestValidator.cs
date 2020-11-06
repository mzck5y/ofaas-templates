using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Service.Handlers
{
    public class SampleRequestValidator : AbstractValidator<SampleRequest>
    {
        #region Constructors

        public SampleRequestValidator()
        {
            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithErrorCode("EMPTY")
                    .WithMessage("{PropertyName} cannot be null or empty")
                .MaximumLength(256)
                    .WithErrorCode("MAX")
                    .WithMessage("{PropertyName} exceed the max length of {MaxLength}.");
        }

        #endregion
    }
}
