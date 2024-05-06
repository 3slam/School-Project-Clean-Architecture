using FluentValidation;
using School.Core.Features.Users.Command.Model;

namespace School.Core.Features.Users.Command.Validations
{
    public class ChangeUserPasswordValidation : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidation()
        {
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("ID cannot be empty.")
           .NotNull().WithMessage("ID is required.");

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
