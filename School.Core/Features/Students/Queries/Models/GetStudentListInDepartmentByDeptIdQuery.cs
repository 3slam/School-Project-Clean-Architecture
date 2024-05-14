using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Result;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentListInDepartmentByDeptIdQuery : IRequest<Response<List<GetStudentListInDepartmentByDeptIdResponse>>>
    {
        public int DepartmentID { get; set; }
    }
}
