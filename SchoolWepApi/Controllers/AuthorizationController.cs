using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Authorization.Command.Model;
using School.Core.Features.Authorization.Queries.Model;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;

namespace SchoolWepApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorizationController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;


        [Authorize(Policy = "Create")]
        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPut(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromBody] EditRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }



        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromQuery] DeleteRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpGet(Router.AuthorizationRouting.GetSingleRole)]
        public async Task<IActionResult> GetSingleRole([FromQuery] GetSingleRoleByIdQuery command)
        {
            return Result(await mediator.Send(command));
        }



        [HttpGet(Router.AuthorizationRouting.GetPaginatedRoleList)]
        public async Task<IActionResult> GetPaginatedRoleList([FromQuery] GetRoleListQuery command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpGet(Router.AuthorizationRouting.GetUserRolesByUserId)]
        public async Task<IActionResult> GetUserRolesByUserId([FromQuery] GetUserRolesByUserIdQuery command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpPut(Router.AuthorizationRouting.UpdateUserRolesByUserId)]
        public async Task<IActionResult> UpdateUserRolesByUserId([FromBody] UpdateUserRolesCommand command)
        {
            return Result(await mediator.Send(command));
        }
    }

}
