
using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Emails.Command.Model
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
