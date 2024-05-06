using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Result;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListResponse>>>
    {

    }
}
