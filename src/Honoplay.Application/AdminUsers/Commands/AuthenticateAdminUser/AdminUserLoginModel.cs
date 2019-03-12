using System;


namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public struct AdminUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }

        public bool IsPasswordExpired { get; }
        public AdminUserAuthenticateModel(int id, string email, string name, bool isPasswordExpired)
        {
            Id = id;
            Email = email;
            Name = name;
            IsPasswordExpired = isPasswordExpired;
        }
    
    }
}
