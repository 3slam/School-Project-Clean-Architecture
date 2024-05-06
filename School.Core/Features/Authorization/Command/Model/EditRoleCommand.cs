using MediatR;
using School.Core.Bases;
using School.Data.Requests;

namespace School.Core.Features.Authorization.Command.Model
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
