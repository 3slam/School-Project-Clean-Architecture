using FluentValidation;
using School.Core.Features.Authentication.Command.Model;

namespace School.Core.Features.Authentication.Command.Validations
{
    public class ConfirmResetPasswordvalidation : AbstractValidator<ConfirmResetPasswordCommand>
    {
        public ConfirmResetPasswordvalidation()
        {

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.Email));

        }


    }
}
