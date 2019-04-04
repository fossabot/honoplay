using Honoplay.AdminWebAPI.Enums;

namespace Honoplay.AdminWebAPI.TestEntities
{
    public class User
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = Roles.Training.ToString();
    }
}
