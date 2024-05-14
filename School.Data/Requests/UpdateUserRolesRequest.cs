using School.Data.Entities;

namespace School.Data.Requests
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }

        public List<UserRole> Roles { get; set; }
    }
}
