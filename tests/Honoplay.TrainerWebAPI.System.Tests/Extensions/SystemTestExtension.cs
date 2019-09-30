﻿using Honoplay.Application.TrainerUsers.Commands.AuthenticateTrainerUser;
using Honoplay.Common.Constants;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Honoplay.TrainerWebAPI.System.Tests.IntegrationTests.Extensions
{
    public static class SystemTestExtension
    {
        public static HttpClient GetTokenAuthorizeHttpClient(CustomWebApplicationFactory<Startup> factory)
        {
            var client = factory.CreateClient();
            var authenticateTrainerUserCommand = new AuthenticateTrainerUserCommand
            {
                Email = "yunuskas55@gmail.com",
                Password = "Passw0rd"
            };
            var content = JsonConvert.SerializeObject(authenticateTrainerUserCommand);
            var httpResponse = client.PostAsync("/TrainerUser/Authenticate", new StringContent(content, Encoding.UTF8, StringConstants.ApplicationJson)).Result;

            var token = JsonConvert.DeserializeObject<Dictionary<string, object>>(httpResponse.Content.ReadAsStringAsync().Result)["token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            return client;
        }
    }
}
