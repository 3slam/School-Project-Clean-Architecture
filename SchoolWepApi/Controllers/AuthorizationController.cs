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


        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.EditRole)]
        public async Task<IActionResult> EditRole([FromBody] EditRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }



        [HttpPost(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.GetSingleRole)]
        public async Task<IActionResult> GetSingleRole([FromBody] GetSingleRoleByIdQuery command)
        {
            return Result(await mediator.Send(command));
        }



        [HttpPost(Router.AuthorizationRouting.GetPaginatedRoleList)]
        public async Task<IActionResult> GetPaginatedRoleList([FromBody] GetRoleListQuery command)
        {
            return Result(await mediator.Send(command));
        }


        [HttpPost(Router.AuthorizationRouting.GetUserRolesByUserId)]
        public async Task<IActionResult> GetUserRolesByUserId([FromBody] GetUserRolesByUserIdQuery command)
        {
            return Result(await mediator.Send(command));
        }
    }

}
