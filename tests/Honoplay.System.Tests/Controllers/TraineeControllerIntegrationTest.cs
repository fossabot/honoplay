using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Honoplay.AdminWebAPI;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Application.Trainees.Commands.CreateTrainee;
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
    }
}
