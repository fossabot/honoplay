#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public readonly struct AdminUserRegisterModel
    {
        public int? Id { get; }

        public string? Email { get; }

        public string? Name { get; }
        public string? Surname { get; }

        public AdminUserRegisterModel(int id, string email, string name, string surname)
        {
            Id = id;
            Email = email;
            Name = name;
            Surname = surname;
        }
    }
}