﻿using Honoplay.AdminWebAPI;
using Honoplay.Application.Options.Commands.CreateOption;
using Honoplay.Application.Options.Commands.UpdateOption;
using Honoplay.Application.Options.Queries.GetOptionsList;
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
    public class OptionControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public OptionControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateOptionAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createOptionCommand = new CreateOptionCommand
            {
                Text = "Asagidakilerden hangisi asagidadir?",
                VisibilityOrder = 1,
                QuestionId = 1,
            };

            var serializedOptionCommand = JsonConvert.SerializeObject(createOptionCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "api/Option",
                content: new StringContent(content: serializedOptionCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateOption()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateOptionCommand = new UpdateOptionCommand
            {
                Id = 1,
                Text = "Yukaridakilerden hangisi aslinda asagidadir?",
                VisibilityOrder = 321,
                QuestionId = 1
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateOptionCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "api/Option",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetOptionsList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getOptionsListQuery = new GetOptionsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"api/Option?Skip={getOptionsListQuery.Skip}&Take={getOptionsListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetOptionDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"api/Option/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
