using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Users.Command.Model
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string UserEmail { set; get; }
    }
}
