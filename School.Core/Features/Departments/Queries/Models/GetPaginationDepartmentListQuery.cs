using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Result;
using School.Core.Wrapper;
using School.Data.Helpers;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetPaginationDepartmentListQuery : IRequest<Response<PaginationResponse<GetDepartmentListResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DepartmentOrderByEnum? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
