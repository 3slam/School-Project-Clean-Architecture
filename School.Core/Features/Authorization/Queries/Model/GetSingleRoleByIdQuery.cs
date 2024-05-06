using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Response;

namespace School.Core.Features.Authorization.Queries.Model
{
    public class GetSingleRoleByIdQuery : IRequest<Response<GetSingleRoleResponse>>
    {
        public string Id { get; set; }
    }
}
