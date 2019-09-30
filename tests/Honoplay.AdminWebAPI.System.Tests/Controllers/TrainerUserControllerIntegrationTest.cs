using Honoplay.AdminWebAPI;
using Honoplay.Application.TrainerUsers.Commands.CreateTrainerUser;
using Honoplay.Application.TrainerUsers.Commands.UpdateTrainerUser;
using Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class TrainerUserControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainerUserControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void CanCreateTrainerUser()
        {

            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var command = new CreateTrainerUserCommand
            {
                Name = "Emre",
                Surname = "KAS",
                PhoneNumber = "12412312312",
                Password = "12412312312",
                DepartmentId = 1,
                CreatedBy = 1,
                ProfessionId = 1,
                Email = "yunuskas21312@gmail.com"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("/TrainerUser", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);

        }
        [Fact]
        public async void CanUpdateTrainerUser()
        {

            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var command = new UpdateTrainerUserCommand
            {
                Id = 1,
                Name = "Yunus Emre",
                Surname = "KAS",
                PhoneNumber = "5555555555",
                Password = "5555555555",
                DepartmentId = 1,
                UpdatedBy = 1,
                ProfessionId = 1,
                Email = "qwdqwdqwdqwd@gmail.com"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PutAsync("/TrainerUser", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        [Fact]
        public async Task CanGetTrainerUserDetail()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await client.GetAsync($"/TrainerUser/1");

            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [Fact]
        public async Task CanGetTrainerUsersList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTrainerUsersListQueryModel = new GetTrainerUsersListQueryModel
            {
                Skip = 0,
                Take = 10,
            };

            var httpResponse = await client.GetAsync($"/TrainerUser?Skip={getTrainerUsersListQueryModel.Skip}&Take={getTrainerUsersListQueryModel.Take}");
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
