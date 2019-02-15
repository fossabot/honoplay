using System;

namespace Honoplay.Application.Tests.Tokens
{
    internal class LoginAdminUserCommand
    {
        public Guid TenantId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}