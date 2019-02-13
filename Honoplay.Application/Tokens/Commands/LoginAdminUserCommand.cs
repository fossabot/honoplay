using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
#nullable enable

namespace Honoplay.Application.Tokens.Commands
{
    public class LoginAdminUserCommand : IRequest<AdminUserLoginModel>
    {
        public Guid TenantId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }
}
