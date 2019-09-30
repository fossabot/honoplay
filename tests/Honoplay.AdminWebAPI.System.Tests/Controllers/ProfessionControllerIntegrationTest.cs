using Honoplay.AdminWebAPI;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.Application.Professions.Commands.CreateProfession;
using Honoplay.Application.Professions.Queries.GetProfessionsList;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class ProfessionControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ProfessionControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CanCreateProfession()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new CreateProfessionCommand
            {
                Professions = new List<string> { "Tasarim" }
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PostAsync(requestUri: "/Profession", content: new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }


        [Fact]
        public async Task CanGetProfessionsList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var query = new GetProfessionsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            //Get professions request
            var httpResponse = await client.GetAsync(requestUri: $"/Profession?Skip={query.Skip}&Take={query.Take}");

            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }
    }
}
