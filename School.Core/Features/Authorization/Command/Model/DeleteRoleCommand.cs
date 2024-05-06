using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authorization.Command.Model
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }

    }
}
