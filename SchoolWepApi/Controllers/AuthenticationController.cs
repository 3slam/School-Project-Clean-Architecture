using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Authentication.Command.Model;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;

namespace SchoolWepApi.Controllers
{
    [ApiController]
    public class AuthenticationController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;

        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            return Result(await mediator.Send(command));
        }
    }
}
