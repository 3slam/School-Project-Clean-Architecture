using MediatR;
using School.Core.Bases;
using School.Core.Features.Authentication.Command.Result;

namespace School.Core.Features.Authentication.Command.Model
{
    public class RefreshTokenCommand : IRequest<Response<RefreshTokenResult>>
    {
        public string refreshToken { get; set; }
    }
}
