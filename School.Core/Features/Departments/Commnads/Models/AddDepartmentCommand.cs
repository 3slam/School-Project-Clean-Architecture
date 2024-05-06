
using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Commnads.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {


        public string DeptName { get; set; }

        public string DeptDesc { get; set; }

        public string DeptLocation { get; set; }

    }
}
