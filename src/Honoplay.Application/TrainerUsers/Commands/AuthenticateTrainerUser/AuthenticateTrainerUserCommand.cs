using MediatR;
using Newtonsoft.Json;

namespace Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser
{
    public class AuthenticateTrainerUserCommand : IRequest<TrainerUserAuthenticateModel>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string HostName { get; set; }
    }
}