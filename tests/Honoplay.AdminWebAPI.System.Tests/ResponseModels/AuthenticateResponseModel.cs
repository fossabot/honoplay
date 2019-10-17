using Newtonsoft.Json;
using System;

namespace Honoplay.AdminWebAPI.System.Tests.ResponseModels
{
    public class AuthenticateResponseModel
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tenantId")]
        public Guid TenantId { get; set; }

        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("isPasswordExpired")]
        public bool IsPasswordExpired { get; set; }
    }
}
