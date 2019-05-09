using Honoplay.AdminWebAPI;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Common.Constants;
using Honoplay.Domain.Entities;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.Application.Tenants.Commands.AddDepartment;
using Xunit;

namespace Honoplay.System.Tests.Controllers
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
                Name = "tenan1",
                HostName = "tenant1",
                Description = "desc",
                CreatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("api/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

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
                Id = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a"),
                HostName = "omega2",
                Name = "test update",
                Description = "desc",
                UpdatedBy = 1
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("api/Tenant", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

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

            var httpResponse = await client.GetAsync($"api/Tenant/{Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a")}");

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

            var httpResponse = await client.GetAsync($"api/Tenant/{Guid.NewGuid()}");

            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTenantsList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await client.GetAsync($"api/Tenant?skip=0&take=10");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [Fact]
        public async Task CanAddDepartment()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new CreateDepartmentCommand
            {
                TenantId = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a"),
                Departments = new List<string>()
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync(requestUri: "api/Tenant/Department", content: new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}