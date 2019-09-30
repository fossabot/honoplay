using System.Net;
using System.Threading.Tasks;
using Honoplay.TrainerWebAPI.System.Tests.Extensions;
using Xunit;

namespace Honoplay.TrainerWebAPI.System.Tests.Controllers
{
    public class TraineeControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TraineeControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanGetTraineeListByClassroomId()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: "/Classroom/1/TraineeUser");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
