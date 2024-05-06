using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Result;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery(int DepartmentId) : IRequest<Response<GetSingleDepartmentResponse>>
    {
        public int DepartmentId = DepartmentId;

    }
}
