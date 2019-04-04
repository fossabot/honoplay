using MediatR;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserCommand : IRequest<AdminUserAuthenticateModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}