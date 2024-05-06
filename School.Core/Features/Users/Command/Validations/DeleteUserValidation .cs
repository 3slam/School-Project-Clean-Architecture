using FluentValidation;
using School.Core.Features.Users.Command.Model;

namespace School.Core.Features.Users.Command.Validations
{
    public class DeleteUserValidation : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidation()
        {


            RuleFor(x => x.UserEmail)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.UserEmail));




        }
    }
}
