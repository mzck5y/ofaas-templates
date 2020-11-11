using FluentValidation;
using Service.Requests;

namespace Service.Validators
{
    public class PostActionRequestValidator : AbstractValidator<PostActionRequest>
    {
        #region Constructors

        public PostActionRequestValidator()
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
