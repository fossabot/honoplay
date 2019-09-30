using Honoplay.AdminWebAPI;
using Honoplay.Application.Questions.Commands.CreateQuestion;
using Honoplay.Application.Questions.Commands.UpdateQuestion;
using Honoplay.Application.Questions.Queries.GetQuestionsList;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.IntegrationTests.Controllers
{
    public class QuestionControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public QuestionControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateQuestionAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createQuestionCommand = new CreateQuestionCommand
            {
                Text = "Asagidakilerden hangisi asagidadir?",
                Duration = 123,
            };

            var serializedQuestionCommand = JsonConvert.SerializeObject(createQuestionCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "api/Question",
                content: new StringContent(content: serializedQuestionCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateQuestion()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateQuestionCommand = new UpdateQuestionCommand
            {
                Id = 1,
                Text = "Yukaridakilerden hangisi aslinda asagidadir?",
                Duration = 321
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateQuestionCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "api/Question",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetQuestionsList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getQuestionsListQuery = new GetQuestionsListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"api/Question?Skip={getQuestionsListQuery.Skip}&Take={getQuestionsListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetQuestionDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"api/Question/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
