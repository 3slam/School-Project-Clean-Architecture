using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;
using School.Core.Wrapper;

namespace School.Core.Features.Authorization.Queries.Model
{
    public class GetRoleListQuery : IRequest<Response<PaginationResponse<GetRoleListResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
