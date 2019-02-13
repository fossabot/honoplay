using System;
#nullable enable

namespace Honoplay.Application.Tokens.Commands
{
    public class AdminUserLoginModel
    {
        public int Id { get; }
        public string? Username { get; }
        public string? Name { get; }
        public Guid? TenantId { get; }

        public bool IsPasswordExpired { get; }
        public AdminUserLoginModel(int id, string username, string name, Guid tenantId, bool isPasswordExpired)
        {
            Id = id;
            Username = username;
            Name = name;
            TenantId = tenantId;
            IsPasswordExpired = isPasswordExpired;
        }
    
    }
}
