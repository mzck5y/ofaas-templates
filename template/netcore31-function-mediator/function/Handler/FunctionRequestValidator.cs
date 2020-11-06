using FluentValidation;

namespace MyFunction.Handler
{
    public class FunctionRequestValidator : AbstractValidator<FunctionRequest>
    {
        #region Constructors

        public FunctionRequestValidator()
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
