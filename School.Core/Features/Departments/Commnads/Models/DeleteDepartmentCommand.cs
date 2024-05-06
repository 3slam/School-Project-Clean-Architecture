using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Commnads.Models
{
    public class DeleteDepartmentCommand : IRequest<Response<string>>
    {
        public int DepartmentId { get; set; }

        public DeleteDepartmentCommand(int departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
