using Honoplay.AdminWebAPI;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class TenantControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TenantControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();

            var authenticateAdminUserCommand = new AuthenticateAdminUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };
            var httpResponse = _client.PostAsync("api/AdminUser/Authenticate", new StringContent(JsonConvert.SerializeObject(authenticateAdminUserCommand), Encoding.UTF8, StringConstants.ApplicationJson)).Result;
            var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResponse.Content.ReadAsStringAsync().Result)["token"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        [Fact]
        public async Task CanCreateTenant()
        {
            // The endpoint or route of the controller action.
            var command = new CreateTenantCommand
            {
                Name = "tenan1",
                HostName = "tenant1",
                Description = "desc",
                CreatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await _client.PostAsync("api/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateTenant()
        {
            // The endpoint or route of the controller action.
            var command = new UpdateTenantCommand
            {
                Id = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a"),
                HostName = "omega2",
                Name = "test update",
                Description = "desc",
                UpdatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await _client.PutAsync("api/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTenant()
        {
            // The endpoint or route of the controller action.

            var httpResponse = await _client.GetAsync($"api/Tenant/{Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a")}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanNotGetTenant()
        {
            // The endpoint or route of the controller action.

            var httpResponse = await _client.GetAsync($"api/Tenant/{Guid.NewGuid()}");

            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTenantsList()
        {
            var httpResponse = await _client.GetAsync($"api/Tenant?skip=0&take=10");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}