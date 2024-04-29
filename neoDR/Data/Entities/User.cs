using Microsoft.AspNetCore.Identity;

namespace neoDR.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool isVIPMember { get; set; }

        public Role.RoleType userLevel { get; set; } = Role.RoleType.Member;

        public List<Role> Roles { get; set; }

    }
}
