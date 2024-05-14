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
        public async Task<IActionResult> SignIn([FromQuery] SignInCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpPost(Router.AuthenticationRouting.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromQuery] RefreshTokenCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthenticationRouting.SendResetPassword)]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpPost(Router.AuthenticationRouting.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthenticationRouting.ResetNewPassword)]
        public async Task<IActionResult> ResetNewPassword([FromQuery] ResetNewPasswordCommand command)
        {
            return Result(await mediator.Send(command));
        }


    }
}
