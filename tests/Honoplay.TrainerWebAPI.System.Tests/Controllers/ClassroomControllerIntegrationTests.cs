using Honoplay.TrainerWebAPI.System.Tests.Extensions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.TrainerWebAPI.System.Tests.Controllers
{
    public class ClassroomControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ClassroomControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanGetClassroomsListByTrainingId()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: "/Training/1/Classroom");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
