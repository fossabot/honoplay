using System;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public readonly struct AdminUserAuthenticateModel
    {
        public int Id { get; }
        public string? Username { get; }
        public string? Name { get; }
        public Guid? TenantId { get; }

        public bool IsPasswordExpired { get; }
        public AdminUserAuthenticateModel(int id, string username, string name, Guid tenantId, bool isPasswordExpired)
        {
            Id = id;
            Username = username;
            Name = name;
            TenantId = tenantId;
            IsPasswordExpired = isPasswordExpired;
        }
    
    }
}
