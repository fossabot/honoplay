using Honoplay.Application.Trainings.Commands.UpdateTraining;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Commands.UpdateTraining
{
    public class UpdateTrainingCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTrainingCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingSeriesId;
        private readonly int _trainingCategoryId;
        private readonly int _trainingId;

        public UpdateTrainingCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingSeriesId, out _trainingCategoryId, out _trainingId);
            _commandHandler = new UpdateTrainingCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int trainingSeriesId, out int trainingCategoryId, out int trainingId)
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

            var trainingCategory = new TrainingCategory
            {
                CreatedBy = adminUser.Id,
                Description = "sample",
                Name = "test"
            };
            context.TrainingCategories.Add(trainingCategory);

            var training = new Training
            {
                TrainingCategoryId = trainingCategory.Id,
                BeginDateTime = DateTimeOffset.Now,
                EndDateTime = DateTimeOffset.Now.AddDays(5),
                CreatedBy = adminUser.Id,
                Description = "description",
                Name = "test",
                TrainingSeriesId = trainingSeries.Id
            };
            context.Trainings.Add(training);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            trainingSeriesId = trainingSeries.Id;
            trainingCategoryId = trainingCategory.Id;
            trainingId = training.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new UpdateTrainingCommand
            {
                Id = _trainingId,
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                TrainingSeriesId = _trainingSeriesId,
                Name = "trainingSample",
                Description = "sampleDescription",
                TrainingCategoryId = _trainingCategoryId,
                BeginDateTime = DateTimeOffset.Now,
                EndDateTime = DateTimeOffset.Now.AddDays(5),
            };

            var trainingResponseModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(trainingResponseModel.Errors);

            Assert.True(trainingResponseModel.Items.Any());
        }

        public void Dispose() => _context?.Dispose();
    }
}
