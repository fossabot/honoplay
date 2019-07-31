﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI;
using Honoplay.Application.Trainings.Commands.CreateTraining;
using Honoplay.Application.Trainings.Commands.UpdateTraining;
using Honoplay.Application.Trainings.Queries.GetTrainingsList;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class TrainingControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainingControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateTrainingAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createTrainingCommand = new CreateTrainingCommand
            {
                CreateTrainingModels = new List<CreateTrainingCommandModel>
                {
                    new CreateTrainingCommandModel
                    {
                        TrainingSeriesId = 1,
                        Name = "trainingSample",
                        Description = "sampleDescription"
                    }
                }
            };

            var serializedTrainingCommand = JsonConvert.SerializeObject(createTrainingCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "api/Training",
                content: new StringContent(content: serializedTrainingCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateTraining()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateTrainingCommand = new UpdateTrainingCommand
            {
                Id = 1,
                TrainingSeriesId = 1,
                Name = "trainingSample",
                Description = "sampleDescription"
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateTrainingCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "api/Training",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTrainingsList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTrainingsListQuery = new GetTrainingsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"api/Training?Skip={getTrainingsListQuery.Skip}&Take={getTrainingsListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTrainingDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"api/Training/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
