using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Result;
using School.Core.Wrapper;
using School.Data.Helpers;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetPaginationStudentListQuery : IRequest<Response<PaginationResponse<GetStudentListResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public StudentOrderByEnum? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
