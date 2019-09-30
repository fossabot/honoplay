using Honoplay.AdminWebAPI;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class TenantControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TenantControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateTenant()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            // The endpoint or route of the controller action.
            var command = new CreateTenantCommand
            {
                Name = "tenaasdasd1",
                HostName = "tasdenant",
                Description = "desasc",
                CreatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }
        [Fact]
        public async Task CanUpdateTenant()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new UpdateTenantCommand
            {
                Id = Guid.Parse("f3878709-3cba-4ed3-4c03-08d70375909d"),
                HostName = "omega2",
                Name = "test update",
                Description = "desc",
                UpdatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTenant()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            // The endpoint or route of the controller action.

            var httpResponse = await client.GetAsync($"/Tenant/{Guid.Parse("f3878709-3cba-4ed3-4c03-08d70375909d")}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanNotGetTenant()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            // The endpoint or route of the controller action.

            var httpResponse = await client.GetAsync($"/Tenant/{Guid.NewGuid()}");

            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTenantsList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await client.GetAsync($"/Tenant?skip=0&take=10");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}