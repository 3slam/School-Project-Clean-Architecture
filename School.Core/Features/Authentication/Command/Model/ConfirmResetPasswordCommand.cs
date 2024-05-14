using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Command.Model
{
    public class ConfirmResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
