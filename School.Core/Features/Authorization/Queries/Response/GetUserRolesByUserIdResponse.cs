
namespace School.Core.Features.Authorization.Queries.Response
{
    public class GetUserRolesByUserIdResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class Role

    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }


        public Role(bool hasRole, string name, string id)
        {
            HasRole = hasRole;
            Name = name;
            Id = id;
        }
    }

}
