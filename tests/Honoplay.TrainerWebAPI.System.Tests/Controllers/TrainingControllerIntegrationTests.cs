using System.Net;
using System.Threading.Tasks;
using Honoplay.TrainerWebAPI.System.Tests.Extensions;
using Xunit;

namespace Honoplay.TrainerWebAPI.System.Tests.Controllers
{
    public class TrainingControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainingControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanGetTrainingListByTrainerId()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: "/Training");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
