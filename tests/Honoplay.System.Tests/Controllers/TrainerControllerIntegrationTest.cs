using Honoplay.AdminWebAPI;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Application.Trainers.Commands.UpdateTrainer;
using Honoplay.Application.Trainers.Queries.GetTrainersList;
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
        public async void CanCreateTrainer()
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
        [Fact]
        public async void CanUpdateTrainer()
        {

            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var command = new UpdateTrainerCommand
            {
                Id = 1,
                Name = "Yunus Emre",
                Surname = "KAS",
                PhoneNumber = "5555555555",
                DepartmentId = 1,
                UpdatedBy = 1,
                ProfessionId = 1,
                Email = "qwdqwdqwdqwd@gmail.com"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PutAsync("api/Trainer", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        [Fact]
        public async Task CanGetTrainerDetail()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await client.GetAsync($"api/Trainer/1");

            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [Fact]
        public async Task CanGetTrainersList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTrainersListQueryModel = new GetTrainersListQueryModel
            {
                Skip = 0,
                Take = 10,
                HostName = "localhost"
            };

            var httpResponse = await client.GetAsync($"api/Trainer?Skip={getTrainersListQueryModel.Skip}&Take={getTrainersListQueryModel.Take}&HostName={getTrainersListQueryModel.HostName}");
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
