using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;

namespace Honoplay.TraineeUserWebAPI.Interfaces
{
    public interface ITraineeUserService
    {
        (TraineeUserAuthenticateModel user, string stringToken) GenerateToken(TraineeUserAuthenticateModel user);
        string RenewToken(string token);
    }
}
