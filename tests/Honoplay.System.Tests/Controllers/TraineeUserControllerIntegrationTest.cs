using Honoplay.AdminWebAPI;
using Honoplay.Application.TraineeUsers.Commands.CreateTraineeUser;
using Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class TraineeUserControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TraineeUserControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async void CanCreateTraineeUser()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);
            // The endpoint or route of the controller action.
            var command = new CreateTraineeUserCommand
            {
                Name = "tenant1",
                DepartmentId = 1,
                Gender = 1,
                CreatedBy = 1,
                Password = "testPass1*",
                Email = "asd@gmail.com",
                PhoneNumber = "65468465466",
                Surname = "tenant-surname",
                NationalIdentityNumber = "879684684654",
                WorkingStatusId = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("api/TraineeUser", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanUpdateTraineeUser()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new UpdateTraineeUserCommand
            {
                Id = 1,
                Name = "tenant1",
                DepartmentId = 1,
                Gender = 1,
                PhoneNumber = "65468465466",
                Password = "teastPass1*",
                Email = "assd@gmail.com",
                Surname = "tenant-surname",
                NationalIdentityNumber = "879684684654",
                WorkingStatusId = 1
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("api/TraineeUser", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanGetTraineeUsersList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTraineeUserListQueryModel = new GetTraineeUsersListQueryModel
            {
                Skip = 0,
                Take = 10,
            };

            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync(requestUri: $"api/TraineeUser?Skip={getTraineeUserListQueryModel.Skip}&Take={getTraineeUserListQueryModel.Take}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanNotGetTraineeUsersList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTraineeUserListQueryModel = new GetTraineeUsersListQueryModel
            {
                Skip = -3,
                Take = -3
            };

            // The endpoint or route of the controller action.
            var httpResponse = await client.GetAsync(requestUri: $"api/TraineeUser?Skip={getTraineeUserListQueryModel.Skip}&Take={getTraineeUserListQueryModel.Take}");

            // Must be unsuccessful.
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanGetTraineeUserDetail()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Request to api/TraineeUser/1 endpoint
            var httpResponse = await client.GetAsync("api/TraineeUser/1");

            //Must be successful.
            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

        }
    }
}
