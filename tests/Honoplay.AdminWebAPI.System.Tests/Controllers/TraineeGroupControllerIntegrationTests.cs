using Honoplay.AdminWebAPI.System.Tests.Extensions;
using Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup;
using Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup;
using Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupsList;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.Controllers
{
    public class TraineeGroupControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TraineeGroupControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateTraineeGroup()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createTraineeGroupCommand = new CreateTraineeGroupCommand
            {
                CreateTraineeGroupCommandModels = new List<CreateTraineeGroupCommandModel>
                {
                    new CreateTraineeGroupCommandModel
                    {
                        Name = "traineeGroupSampleeee",
                    }
                }
            };

            var serializedTraineeGroupCommand = JsonConvert.SerializeObject(createTraineeGroupCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "/TraineeGroup",
                content: new StringContent(content: serializedTraineeGroupCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateTraineeGroup()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateTraineeGroupCommand = new UpdateTraineeGroupCommand
            {
                UpdateTraineeGroupCommandModels = new List<UpdateTraineeGroupCommandModel>
                {
                    new UpdateTraineeGroupCommandModel
                    {
                        Name = "traineeGroupSample",
                    }
                }
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateTraineeGroupCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "/TraineeGroup",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTraineeGroupsList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTraineeGroupsListQuery = new GetTraineeGroupsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"/TraineeGroup?Skip={getTraineeGroupsListQuery.Skip}&Take={getTraineeGroupsListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTraineeGroupDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"/TraineeGroup/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
