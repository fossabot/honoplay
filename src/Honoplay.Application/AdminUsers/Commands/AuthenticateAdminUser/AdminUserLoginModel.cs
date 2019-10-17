using System;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public struct AdminUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public Guid TenantId { get; }
        public string HostName { get; }
        public bool IsPasswordExpired { get; }

        public AdminUserAuthenticateModel(int id, string email, string name, Guid tenantId, string hostName, bool isPasswordExpired)
        {
            Id = id;
            Email = email;
            Name = name;
            TenantId = tenantId;
            HostName = hostName;
            IsPasswordExpired = isPasswordExpired;
        }
    }
}
