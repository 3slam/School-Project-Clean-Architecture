using FluentValidation;
using School.Core.Features.Emails.Command.Model;

namespace School.Core.Features.Emails.Command.Validations
{
    public class SendEmailValidation : AbstractValidator<SendEmailCommand>
    {
        public SendEmailValidation()
        {

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email address is required.")
           .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
           .WithMessage("Invalid email address format must be like a12@gmail.com")
           .When(x => !string.IsNullOrEmpty(x.Email));


            RuleFor(x => x.Body)
           .NotEmpty().WithMessage("Email Body is required.");

            RuleFor(x => x.Subject)
           .NotEmpty().WithMessage("Email Subject is required.");

        }
    }
}
