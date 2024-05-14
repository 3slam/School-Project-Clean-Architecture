using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Command.Model
{
    public class ConfirmEmailCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string CodeConfirmation { get; set; }
    }
}
