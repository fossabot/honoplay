using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Honoplay.AdminWebAPI.System.Tests.Extensions
{
    public static class SystemTestExtension
    {
        public static HttpClient GetTokenAuthorizeHttpClient(CustomWebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            var authenticateAdminUserCommand = new AuthenticateAdminUserCommand
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd"
            };
            var content = JsonConvert.SerializeObject(authenticateAdminUserCommand);
            var httpResponse = client.PostAsync("/adminuser/authenticate", new StringContent(content, Encoding.UTF8, StringConstants.ApplicationJson)).Result;

            var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResponse.Content.ReadAsStringAsync().Result)["token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            return client;
        }
    }
}
