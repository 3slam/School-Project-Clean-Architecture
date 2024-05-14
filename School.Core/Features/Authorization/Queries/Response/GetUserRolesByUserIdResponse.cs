
using School.Data.Entities;

namespace School.Core.Features.Authorization.Queries.Response
{
    public class GetUserRolesByUserIdResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserRole> Roles { get; set; }
    }


}
