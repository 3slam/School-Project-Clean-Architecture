using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Features.Authorization.Queries.Model
{
    public class GetUserRolesByUserIdQuery :
        IRequest<Response<GetUserRolesByUserIdResponse>>
    {
        public string Id { get; set; }
    }
}
