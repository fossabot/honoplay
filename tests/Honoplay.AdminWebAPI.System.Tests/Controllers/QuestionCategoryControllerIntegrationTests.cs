using Honoplay.AdminWebAPI.System.Tests.Extensions;
using Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory;
using Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory;
using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList;
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
    public class QuestionCategoryControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public QuestionCategoryControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateQuestionCategoryAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createQuestionCategoryCommand = new CreateQuestionCategoryCommand
            {
                CreateQuestionCategoryModels = new List<CreateQuestionCategoryCommandModel>
                {
                    new CreateQuestionCategoryCommandModel
                    {
                        Name = "questionCategoryqdqwd1",
                    }
                }
            };

            var serializedQuestionCategoryCommand = JsonConvert.SerializeObject(createQuestionCategoryCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "/QuestionCategory",
                content: new StringContent(content: serializedQuestionCategoryCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateQuestionCategory()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateQuestionCategoryCommand = new UpdateQuestionCategoryCommand
            {
                Id = 1,
                Name = "questionCategory1",
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateQuestionCategoryCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "/QuestionCategory",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetQuestionCategoriesList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getQuestionCategoriesListQuery = new GetQuestionCategoriesListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"/QuestionCategory?Skip={getQuestionCategoriesListQuery.Skip}&Take={getQuestionCategoriesListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

    }
}
