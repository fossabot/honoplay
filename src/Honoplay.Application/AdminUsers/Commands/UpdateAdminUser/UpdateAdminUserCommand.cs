using Honoplay.Application._Infrastructure;
using MediatR;

namespace Honoplay.Application.AdminUsers.Commands.UpdateAdminUser
{
    public class UpdateAdminUserCommand : IRequest<ResponseModel<UpdateAdminUserModel>>
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public int TimeZone { get; set; }
    }
}