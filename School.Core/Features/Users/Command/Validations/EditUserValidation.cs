using FluentValidation;
using School.Core.Features.Users.Command.Model;

namespace School.Core.Features.Users.Command.Validations
{
    public class EditUserValidation : AbstractValidator<EditUserCommand>
    {
        public EditUserValidation()
        {
            RuleFor(x => x.Id)
          .NotEmpty().WithMessage("ID cannot be empty.")
          .NotNull().WithMessage("ID is required.");


            RuleFor(x => x.FullName)
              .NotEmpty().WithMessage("Full name is required.")
             .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.")
              .When(x => !string.IsNullOrEmpty(x.FullName));

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100).WithMessage("Username cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.UserName));

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.Email));


        }
    }
}
