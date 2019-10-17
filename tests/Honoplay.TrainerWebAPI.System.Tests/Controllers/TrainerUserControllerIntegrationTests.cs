using Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.Application.TraineeUsers.Commands.AuthenticateTraineeUser;
using Honoplay.TrainerWebAPI.System.Tests.Extensions;
using Honoplay.TrainerWebAPI.System.Tests.ResponseModels;
using Xunit;

namespace Honoplay.TrainerWebAPI.System.Tests.Controllers
{
    public class TrainerUserControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainerUserControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanLoginTrainerUser()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            // The endpoint or route of the controller action.
            var command = new AuthenticateTrainerUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await authorizedClient.PostAsync("/TrainerUser/Authenticate", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [Fact]
        public async Task CanRenewTokenTrainerUser()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            // The endpoint or route of the controller action.
            var command = new AuthenticateTrainerUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };
            var json = JsonConvert.SerializeObject(command);
            var authenticateResponse = await client.PostAsync("/TrainerUser/Authenticate", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            var firstToken = JsonConvert.DeserializeObject<AuthenticateResponseModel>(await authenticateResponse.Content.ReadAsStringAsync()).Token;

            //Second request to renewToken

            var renewToken = JsonConvert.SerializeObject(firstToken);
            var renewTokenResponse = await client.PostAsync("/TrainerUser/RenewToken", new StringContent(renewToken, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            renewTokenResponse.EnsureSuccessStatusCode();

            Assert.True(renewTokenResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, renewTokenResponse.StatusCode);
        }
    }
}