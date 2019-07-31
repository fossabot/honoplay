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

namespace Honoplay.Application.Tests.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTrainingsListQueryHandler _getTrainingsListQueryHandler;
        private readonly Guid _tenantId;

        public GetTrainingsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getTrainingsListQueryHandler = new GetTrainingsListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId)
        {
            var context = GetDbContext();
            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Id = 1,
                Email = "test@omegabigdata.com",
                Password = "pass".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTimeOffset.Now.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);


            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUser.Id
            };
            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var question = new Question
            {
                Duration = 3,
                Text = "testQuestion",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            };
            context.Questions.Add(question);

            var trainingSeries = new TrainingSeries
            {
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id,
                Name = "testTrainingSeries"
            };
            context.TrainingSerieses.Add(trainingSeries);

            var training = new Training
            {
                CreatedBy = adminUser.Id,
                Description = "description",
                Name = "test",
                TrainingSeriesId = trainingSeries.Id
            };
            context.Trainings.Add(training);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getTrainingsListQuery = new GetTrainingsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var trainingSeriesesModel = await _getTrainingsListQueryHandler.Handle(getTrainingsListQuery, CancellationToken.None);

            Assert.Null(trainingSeriesesModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getTrainingsListQuery = new GetTrainingsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var trainingSeriesesModel = await _getTrainingsListQueryHandler.Handle(getTrainingsListQuery, CancellationToken.None);

            Assert.Single(trainingSeriesesModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
