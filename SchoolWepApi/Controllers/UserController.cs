using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Users.Command.Model;
using School.Core.Features.Users.Queries.Models;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;

namespace SchoolWepApi.Controllers
{
    [ApiController]

    public class UserController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;


        [HttpPost(Router.UserRouting.CreateUser)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPut(Router.UserRouting.EditeUser)]
        public async Task<IActionResult> EditeUser([FromBody] EditUserCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpDelete(Router.UserRouting.DeleteUser)]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPut(Router.UserRouting.ChangeUserPassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpGet(Router.UserRouting.GetUsersList)]
        public async Task<IActionResult> GetUsersList([FromQuery] GetAllUsersListPaginationQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet(Router.UserRouting.GetSingleUserByEmail)]
        public async Task<IActionResult> GetSingleUserByEmail([FromQuery] GetUserByEmailQuery query)
        {
            return Ok(await mediator.Send(query));
        }


    }
}
