using System;
using System.Collections.Generic;
using Honoplay.Domain.Entities;

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
        public string JsValidators { get; }

        public AdminUserAuthenticateModel(int id, string email, string name, bool isPasswordExpired, Guid tenantId, string hostName, string jsValidators)
        {
            Id = id;
            Email = email;
            Name = name;
            IsPasswordExpired = isPasswordExpired;
            TenantId = tenantId;
            HostName = hostName;
            JsValidators = jsValidators;
        }
    }
}
