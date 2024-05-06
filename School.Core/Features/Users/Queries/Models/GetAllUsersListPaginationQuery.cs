using MediatR;
using School.Core.Bases;
using School.Core.Features.Users.Queries.Response;
using School.Core.Wrapper;

namespace School.Core.Features.Users.Queries.Models
{
    public class GetAllUsersListPaginationQuery :
        IRequest<Response<PaginationResponse<GetUserPaginationReponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? searach { get; set; }

    }
}
