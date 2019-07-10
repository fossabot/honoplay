using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser
{
    public class AuthenticateAdminUserCommand : IRequest<AdminUserAuthenticateModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string HostName { get; set; }
    }
}