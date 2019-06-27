using System;
using System.Collections.Generic;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public struct AdminUserAuthenticateModel
    {
        public int Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string Tenants { get; }
        public bool IsPasswordExpired { get; }
        public string JsValidators { get; }

        public AdminUserAuthenticateModel(int id, string email, string name, bool isPasswordExpired, List<Guid> tenants, string jsValidators)
        {
            Id = id;
            Email = email;
            Name = name;
            IsPasswordExpired = isPasswordExpired;
            Tenants = string.Join(",", tenants);
            JsValidators = jsValidators;
        }
    }
}
