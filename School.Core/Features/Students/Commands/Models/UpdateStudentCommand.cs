using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Students.Commands.Models
{
    public class UpdateStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
