﻿
using MediatR;
using Microsoft.AspNetCore.Http;
using School.Core.Bases;


namespace School.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
