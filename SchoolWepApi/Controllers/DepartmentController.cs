using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Departments.Commnads.Models;
using School.Core.Features.Departments.Queries.Models;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;


namespace SchoolWepApi.Controllers
{

    [ApiController]
    public class DepartmentController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;

        [HttpGet(Router.DepartmentRouting.GetAllDepartments)]
        public async Task<IActionResult> GetAllDepartments()
        {
            return Result(await mediator.Send(new GetDepartmentListQuery()));
        }

        [HttpGet(Router.DepartmentRouting.GetDepartmentById)]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int departmentId)
        {
            return Result(await mediator.Send(new GetDepartmentByIdQuery(departmentId)));
        }

        [HttpPost(Router.DepartmentRouting.CreateDepartment)]
        public async Task<IActionResult> CreateDepartment([FromBody] AddDepartmentCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpDelete(Router.DepartmentRouting.DeleteDepartment)]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int departmentId)
        {
            return Result(await mediator.Send(new DeleteDepartmentCommand(departmentId)));
        }

        [HttpGet(Router.DepartmentRouting.GetAllDepartmentUsingPagination)]
        public async Task<IActionResult> GetAllDepartmentUsingPagination([FromQuery] GetPaginationDepartmentListQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
