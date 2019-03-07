using Honoplay.Application.Infrastructure;
using MediatR;

#nullable enable

namespace Honoplay.Application.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserCommand : IRequest<ResponseModel<AdminUserRegisterModel>>
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }

        public int TimeZone { get; set; }
    }
}