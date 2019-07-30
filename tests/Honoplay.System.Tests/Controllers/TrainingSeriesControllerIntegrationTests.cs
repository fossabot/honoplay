using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI;
using Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries;
using Honoplay.Application.TrainingSerieses.Commands.UpdateTrainingSeries;
using Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class TrainingSeriesControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainingSeriesControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateTrainingSeriesAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createTrainingSeriesCommand = new CreateTrainingSeriesCommand
            {
                CreateTrainingSeriesModels = new List<CreateTrainingSeriesCommandModel>
                {
                    new CreateTrainingSeriesCommandModel
                    {
                        Name = "trainingSeries1"
                    }
                }
            };

            var serializedTrainingSeriesCommand = JsonConvert.SerializeObject(createTrainingSeriesCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "api/TrainingSeries",
                content: new StringContent(content: serializedTrainingSeriesCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateTrainingSeries()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateTrainingSeriesCommand = new UpdateTrainingSeriesCommand
            {
                Id = 1,
                Name = "sample"
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateTrainingSeriesCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "api/TrainingSeries",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTrainingSeriessList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTrainingSeriesesListQuery = new GetTrainingSeriesesListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"api/TrainingSeries?Skip={getTrainingSeriesesListQuery.Skip}&Take={getTrainingSeriesesListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTrainingSeriesDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"api/TrainingSeries/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
