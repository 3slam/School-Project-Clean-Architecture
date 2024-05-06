using FluentValidation;
using School.Core.Features.Authentication.Command.Model;

namespace School.Core.Features.Authentication.Command.Validations
{
    public class SignInValidation : AbstractValidator<SignInCommand>
    {
        public SignInValidation()
        {

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.Email));


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
