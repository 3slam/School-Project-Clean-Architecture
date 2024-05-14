using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Emails.Command.Model;
using School.Core.Features.Emails.Command.Validations;
using School.Data.Resourses;
using School.Service.IService;

namespace School.Core.Features.Emails.Command.Handler
{
    public class EmailCommandHandler(
        IEmailService emailService,
       IStringLocalizer<SharedResourses> localizer,
       IMapper mapper) : ResponseHandler(localizer)
        , IRequestHandler<SendEmailCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var validator = new SendEmailValidation();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                string error = "";
                foreach (var item in validationResult.Errors)
                    error = error + item.ErrorMessage;
                return BadRequest<string>(error);
            }

            var result = await emailService.SendEmailAsync
                (request.Email, request.Subject, request.Body);

            if (result) return Success("Email send Successfully.");
            return BadRequest<string>("Email did not send Successfully.");
        }
    }
}
