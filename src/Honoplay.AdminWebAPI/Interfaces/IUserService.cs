using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;

namespace Honoplay.AdminWebAPI.Interfaces
{
    public interface IUserService
    {
        (AdminUserAuthenticateModel user, string stringToken) GenerateToken(AdminUserAuthenticateModel user);
        string RenewToken(string token);
    }
}
