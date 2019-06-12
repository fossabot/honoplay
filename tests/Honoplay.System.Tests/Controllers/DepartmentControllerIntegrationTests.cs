using Honoplay.AdminWebAPI;
using Honoplay.Application.Departments.Commands.CreateDepartment;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{

    public class DepartmentControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DepartmentControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CanCreateDepartment()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new CreateDepartmentCommand
            {
                Departments = new List<string> { "Tasarim" }
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
