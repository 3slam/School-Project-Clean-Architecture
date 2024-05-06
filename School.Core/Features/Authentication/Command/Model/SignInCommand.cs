using MediatR;
using School.Core.Bases;
using School.Data.Entities;

namespace School.Core.Features.Authentication.Command.Model
{
    public class SignInCommand : IRequest<Response<JwtToken>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
