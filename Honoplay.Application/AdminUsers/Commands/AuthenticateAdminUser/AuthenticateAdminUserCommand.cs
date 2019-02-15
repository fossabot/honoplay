using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserCommand : IRequest<AdminUserAuthenticateModel>
    {
        public Guid TenantId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}
