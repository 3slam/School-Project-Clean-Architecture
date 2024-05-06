using MediatR;
using School.Core.Bases;
using School.Core.Features.Users.Queries.Response;
using System.ComponentModel.DataAnnotations;

namespace School.Core.Features.Users.Queries.Models
{
    public class GetUserByEmailQuery() : IRequest<Response<GetUserByEmailResponse>>
    {
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
