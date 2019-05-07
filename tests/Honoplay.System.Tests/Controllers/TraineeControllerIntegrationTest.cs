﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI;
using Honoplay.Application.Trainees.Commands.CreateTrainee;
using Honoplay.Application.Trainees.Queries.GetTraineeList;
using Honoplay.Common.Constants;
using Honoplay.System.Tests.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace Honoplay.System.Tests.Controllers
{
    public class TraineeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TraineeControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task CanCreateTrainee()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);
            // The endpoint or route of the controller action.
            var command = new CreateTraineeCommand
            {
                Name = "tenant1",
                DepartmentId = 1,
                Gender = 1,
                CreatedBy = 1,
                PhoneNumber = "65468465466",
                Surname = "tenant-surname",
                NationalIdentityNumber = "879684684654",
                WorkingStatusId = 1
            };

            var json = JsonConvert.SerializeObject(command);

            var httpResponse = await client.PostAsync("api/Trainee", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateTrainee()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            //Init model
            var command = new UpdateTraineeCommand
            {
                Id = 1,
                Name = "tenant1",
                DepartmentId = 1,
                Gender = 1,
                PhoneNumber = "65468465466",
                Surname = "tenant-surname",
                NationalIdentityNumber = "879684684654",
                WorkingStatusId = 1
            };

            var json = JsonConvert.SerializeObject(command);

            // The endpoint or route of the controller action.
            var httpResponse = await client.PutAsync("api/Trainee", new StringContent(json, Encoding.UTF8, StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanGetTraineeList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTraineeListQueryModel = new GetTraineeListQueryModel
            {
                Skip = 0,
                Take = 10
            };
            // The endpoint or route of the controller action.

            var httpResponse = await client.GetAsync(requestUri: $"api/Trainee?Skip={getTraineeListQueryModel.Skip}&Take={getTraineeListQueryModel.Take}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async void CanNotGetTraineeList()
        {
            var client = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTraineeListQueryModel = new GetTraineeListQueryModel
            {
                Skip = -3,
                Take = -3
            };
            // The endpoint or route of the controller action.

            var httpResponse = await client.GetAsync(requestUri: $"api/Trainee?Skip={getTraineeListQueryModel.Skip}&Take={getTraineeListQueryModel.Take}");

            // Must be unsuccessful.
            Assert.False(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
        }
    }
}
