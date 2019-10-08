using System;
using System.Collections.Generic;
using Honoplay.AdminWebAPI;
using Honoplay.Application.ContentFiles.Commands.CreateContentFile;
using Honoplay.Application.ContentFiles.Commands.UpdateContentFile;
using Honoplay.Application.ContentFiles.Queries.GetContentFilesList;
using Honoplay.Common.Constants;
using Honoplay.AdminWebAPI.System.Tests.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.AdminWebAPI.System.Tests.Controllers
{
    public class ContentFileControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ContentFileControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanCreateContentFileAsync()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var createContentFileCommand = new CreateContentFileCommand
            {
                CreateContentFileModels = new List<CreateContentFileCommandModel>
                {
                    new CreateContentFileCommandModel
                    {
                        Name = "contentFile1",
                        ContentType = "contentFile1",
                        Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    }
                }
            };

            var serializedContentFileCommand = JsonConvert.SerializeObject(createContentFileCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PostAsync(requestUri: "/ContentFile",
                content: new StringContent(content: serializedContentFileCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateContentFile()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var updateContentFileCommand = new UpdateContentFileCommand
            {
                Id = Guid.Parse("4f2b88e3-704c-41d8-a679-f608a159d055"),
                Name = "contentFile1",
                ContentType = "contentFile1",
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            var serializedUpdateCommand = JsonConvert.SerializeObject(updateContentFileCommand);

            // The endpoint or route of the controller action.
            var httpResponse = await authorizedClient.PutAsync(requestUri: "/ContentFile",
                    content: new StringContent(content: serializedUpdateCommand,
                    encoding: Encoding.UTF8,
                    mediaType: StringConstants.ApplicationJson));

            //Must be successful
            httpResponse.EnsureSuccessStatusCode();

            Assert.True(httpResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetContentFilesList()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var getContentFilesListQuery = new GetContentFilesListQueryModel
            {
                Skip = 0,
                Take = 10
            };

            var httpResponse = await authorizedClient.GetAsync(
                requestUri: $"/ContentFile?Skip={getContentFilesListQuery.Skip}&Take={getContentFilesListQuery.Take}");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task CanGetContentFileDetail()
        {
            var authorizedClient = SystemTestExtension.GetTokenAuthorizeHttpClient(_factory);

            var httpResponse = await authorizedClient.GetAsync(requestUri: $"/ContentFile/4f2b88e3-704c-41d8-a679-f608a159d055");

            httpResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
