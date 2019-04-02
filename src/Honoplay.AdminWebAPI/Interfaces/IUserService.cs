using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;

namespace Honoplay.AdminWebAPI.Interfaces
{
    public interface IUserService
    {
        (AdminUserAuthenticateModel user, string stringToken) Authenticate(AdminUserAuthenticateModel user);
    }
}
