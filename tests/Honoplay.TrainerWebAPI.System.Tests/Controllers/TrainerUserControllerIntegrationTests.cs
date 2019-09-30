using Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.TrainerWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class TrainerUserControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TrainerUserControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanLoginTrainerUser()
        {
            // The endpoint or route of the controller action.
            var command = new AuthenticateTrainerUserCommand
            {
                Email = "yunuskas55@gmail.com",
                Password = "Passw0rd"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await _client.PostAsync("/TrainerUser/Authenticate", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}