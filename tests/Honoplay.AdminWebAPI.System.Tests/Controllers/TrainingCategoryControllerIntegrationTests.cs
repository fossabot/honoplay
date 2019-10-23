using Honoplay.AdminWebAPI.System.Tests.Extensions;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.Controllers
{
    public class TrainingCategoryControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainingCategoryControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanGetTrainingCategoriesList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getTrainingCategoriesListQuery = new GetTrainingCategoriesListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"/TrainingCategory?Skip={getTrainingCategoriesListQuery.Skip}&Take={getTrainingCategoriesListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetTrainingCategoryDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"/TrainingCategory/1");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
