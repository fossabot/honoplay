using Honoplay.AdminWebAPI.System.Tests.ResponseModels;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.Controllers
{
    public class AdminUserControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AdminUserControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanRegisterAdminUser()
        {
            // The endpoint or route of the controller action.
            var command = new RegisterAdminUserCommand
            {
                Email = "test1@omegabigdata.com",
                Password = "Passw0rd11",
                Name = "Test",
                Surname = "Test",
                TimeZone = 0
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await _client.PostAsync("/AdminUser/Register", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanLoginAdminUser()
        {
            // The endpoint or route of the controller action.
            var command = new AuthenticateAdminUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await _client.PostAsync("/AdminUser/Authenticate", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanRenewTokenAdminUser()
        {
            //First request to get Token.
            var command = new AuthenticateAdminUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };
            var json = JsonConvert.SerializeObject(command);
            var authenticateResponse = await _client.PostAsync("/AdminUser/Authenticate", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            var firstToken = JsonConvert.DeserializeObject<AuthenticateResponseModel>(await authenticateResponse.Content.ReadAsStringAsync()).Token;

            //Second request to renewToken

            var renewToken = JsonConvert.SerializeObject(firstToken);
            var renewTokenResponse = await _client.PostAsync("/AdminUser/RenewToken", new StringContent(renewToken, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            renewTokenResponse.EnsureSuccessStatusCode();

            Assert.True(renewTokenResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, renewTokenResponse.StatusCode);
        }
    }
}