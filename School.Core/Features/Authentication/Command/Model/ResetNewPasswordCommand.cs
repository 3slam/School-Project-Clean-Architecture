using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Command.Model
{
    public class ResetNewPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
