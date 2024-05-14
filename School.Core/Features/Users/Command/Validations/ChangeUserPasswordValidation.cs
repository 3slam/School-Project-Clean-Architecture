using FluentValidation;
using School.Core.Features.Users.Command.Model;

namespace School.Core.Features.Users.Command.Validations
{
    public class ChangeUserPasswordValidation : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidation()
        {
            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.Email));


            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password cannot be empty.")
                .NotNull().WithMessage("Current password is required.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password cannot be empty.")
                .NotNull().WithMessage("New password is required.");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");

        }
    }
}
