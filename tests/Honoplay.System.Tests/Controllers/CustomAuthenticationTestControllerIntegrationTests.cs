using Honoplay.AdminWebAPI;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class CustomAuthenticationTestControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CustomAuthenticationTestControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        // [Fact]
        // public async void CanNotGetValuesAsync()
        // {
        //     var httpResponse = await _client.GetAsync("api/CustomAuthenticationTest");

        //     Assert.False(httpResponse.IsSuccessStatusCode);
        //     Assert.Equal(HttpStatusCode.Unauthorized, httpResponse.StatusCode);
        // }

        // [Fact]
        // public async void CanGetValuesAsync()
        // {
        //     AuthenticateAdminUserCommand command = new AuthenticateAdminUserCommand
        //     {
        //         Email = "registered@omegabigdata.com",
        //         Password = "Passw0rd"
        //     };

        //     var json = JsonConvert.SerializeObject(command);

        //     //Authenticate and init session
        //     await _client.PostAsync("api/CustomAuthenticationTest", new StringContent(json, Encoding.UTF8, "application/json"));

        //     //So now we can access the action
        //     var httpResponse = await _client.GetAsync("api/CustomAuthenticationTest");

        //     Assert.True(httpResponse.IsSuccessStatusCode);
        //     Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        // }
    }
}
