using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Commnads.Models
{
    public class UpdateDepartmentCommand : IRequest<Response<string>>
    {

        public int DepartmentId { get; set; }
        public string DeptName { get; set; }

        public string DeptDesc { get; set; }


        public string DeptLocation { get; set; }

    }
}
