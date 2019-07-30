using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public class UpdateTrainingSeriesCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTrainingSeriesCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingSeriesId;

        public UpdateTrainingSeriesCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingSeriesId);
            _commandHandler = new UpdateTrainingSeriesCommandHandler(_context, new CacheManager(cache.Object));
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
                Name = "testTrainingSeries"
            };

            _context.TrainingSerieses.Add(trainingSeries);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            trainingSeriesId = trainingSeries.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateTrainingSeriesCommand = new UpdateTrainingSeriesCommand
            {
                Id = _trainingSeriesId,
                Name = "updateTest",
                UpdatedId = _adminUserId,
                TenantId = _tenantId
            };

            var trainingSeriesModel = await _commandHandler.Handle(updateTrainingSeriesCommand, CancellationToken.None);

            Assert.Null(trainingSeriesModel.Errors);
            Assert.Equal(expected: updateTrainingSeriesCommand.Name, actual: trainingSeriesModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateTrainingSeriesCommand = new UpdateTrainingSeriesCommand
            {
                Id = _trainingSeriesId+1,
                Name = "updateTest",
                UpdatedId = _adminUserId,
                TenantId = _tenantId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateTrainingSeriesCommand, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
