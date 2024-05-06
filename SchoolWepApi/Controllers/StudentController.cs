﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Models;
using School.Data.AppMetaData;
using SchoolWepApi.Bases;


namespace SchoolWepApi.Controllers
{

    [ApiController]
    public class StudentController(IMediator mediator) : AppController
    {
        private readonly IMediator mediator = mediator;

        [HttpGet(Router.StudentRouting.GetAllStudnets)]
        public async Task<IActionResult> getAllStudents()
        {
            return Result(await mediator.Send(new GetStudentListQuery()));
        }

        [HttpGet(Router.StudentRouting.GetStudentById)]
        public async Task<IActionResult> getAllStudents([FromRoute] int studentId)
        {
            return Result(await mediator.Send(new GetStudentByIdQuery(studentId)));
        }

        [HttpPost(Router.StudentRouting.CreateStudnet)]
        public async Task<IActionResult> CreateStudnet([FromBody] AddStudentCommand command)
        {
            return Result(await mediator.Send(command));
        }

        [HttpDelete(Router.StudentRouting.DeleteStudent)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int studentId)
        {
            return Result(await mediator.Send(new DeleteStudentCommand(studentId)));
        }

        [HttpGet(Router.StudentRouting.GetAllStudnetsUsingPagination)]
        public async Task<IActionResult> GetAllStudnetsUsingPagination
            ([FromQuery] GetPaginationStudentListQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
