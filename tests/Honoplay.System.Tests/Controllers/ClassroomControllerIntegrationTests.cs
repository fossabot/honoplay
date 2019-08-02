using Honoplay.AdminWebAPI;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.Application.Classrooms.Commands.CreateClassroom;
using Honoplay.Application.Classrooms.Commands.UpdateClassroom;
using Honoplay.Application.Classrooms.Queries.GetClassroomsList;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class ClassroomControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ClassroomControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateClassroomAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createClassroomCommand = new CreateClassroomCommand
            {
                CreateClassroomModels = new List<CreateClassroomCommandModel>
                {
                    new CreateClassroomCommandModel
                    {
                        TrainerId = 1,
                        TrainingId = 1,
                        Name = "test"
                    }
                }
            };

            var serializedClassroomCommand = JsonConvert.SerializeObject(createClassroomCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "api/Classroom",
                content: new StringContent(content: serializedClassroomCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateClassroom()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateClassroomCommand = new UpdateClassroomCommand
            {
                Id = 1,
                TrainerId = 1,
                TrainingId = 1,
                Name = "test"
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateClassroomCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "api/Classroom",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetClassroomsList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getClassroomsListQuery = new GetClassroomsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"api/Classroom?Skip={getClassroomsListQuery.Skip}&Take={getClassroomsListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetClassroomDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"api/Classroom/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
