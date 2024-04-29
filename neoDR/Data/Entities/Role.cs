namespace neoDR.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public enum RoleType
        {
            Admin = 1,
            VIP = 2,
            Member = 3
        }

    }
}
