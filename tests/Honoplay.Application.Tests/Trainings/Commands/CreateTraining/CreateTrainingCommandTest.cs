using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Commands.CreateTraining
{
    public class CreateTrainingCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly TrainingCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingSeriesId;

        public CreateTrainingCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingSeriesId);
            _commandHandler = new TrainingCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int trainingSeriesId)
        {
            var context = GetDbContext();

            var salt = ByteArrayExtensions.GetRandomSalt();
            var adminUser = new AdminUser
            {
                Id = 1,
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
            };
            context.AdminUsers.Add(adminUser);

            var tenant = new Tenant
            {
                Name = "TestTenant#01",
                HostName = "localhost"
            };

            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var trainingSeries = new TrainingSeries
            {
                TenantId = tenant.Id,
                CreatedBy = adminUser.Id,
                Name = "testSeries"
            };

            context.TrainingSerieses.Add(trainingSeries);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            trainingSeriesId = trainingSeries.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new TrainingCommand
            {
                CreatedBy = _adminUserId,
                TenantId = _tenantId,
                TrainingModels = new List<TrainingCommandModel>
                {
                    new TrainingCommandModel
                    {
                        TrainingSeriesId = _trainingSeriesId,
                        Name = "trainingSample",
                        Description = "sampleDescription"
                    }
                }
            };

            var trainingResponseModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(trainingResponseModel.Errors);

            Assert.True(trainingResponseModel.Items.Single().Count > 0);
        }

        public void Dispose() => _context?.Dispose();
    }
}
