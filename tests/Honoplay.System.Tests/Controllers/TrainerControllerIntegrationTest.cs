using Honoplay.AdminWebAPI;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var command = new CreateTrainerCommand
            {
                Name = "Emre",
                Surname = "KAS",
                PhoneNumber = "12412312312",
                DepartmentId = 1,
                CreatedBy = 1,
                ProfessionId = 1,
                Email = "yunuskas21312@gmail.com"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("api/Trainer", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

        }
    }
}
