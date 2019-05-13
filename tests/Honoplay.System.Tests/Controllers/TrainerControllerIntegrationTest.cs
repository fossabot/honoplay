using System.Threading.Tasks;
using Honoplay.AdminWebAPI;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class TrainerControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainerControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateTrainer()
        {


        }
    }
}
