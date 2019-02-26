using MediatR;

#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserCommand : IRequest<AdminUserAuthenticateModel>
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }
    }
}