namespace School.Data.Entities
{
    public class UserRole
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }


        public UserRole(bool hasRole, string name, string id)
        {
            HasRole = hasRole;
            Name = name;
            Id = id;
        }


    }
}
