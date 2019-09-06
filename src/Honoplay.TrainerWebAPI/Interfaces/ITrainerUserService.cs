using Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser;

namespace Honoplay.TrainerWebAPI.Interfaces
{
    public interface ITrainerUserService
    {
        (TrainerUserAuthenticateModel user, string stringToken) GenerateToken(TrainerUserAuthenticateModel user);
        string RenewToken(string token);
    }
}
