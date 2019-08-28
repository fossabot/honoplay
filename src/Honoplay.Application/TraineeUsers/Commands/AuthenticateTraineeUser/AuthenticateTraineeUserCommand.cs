using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser
{
    public class AuthenticateTraineeUserCommand : IRequest<TraineeUserAuthenticateModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string HostName { get; set; }
    }
}