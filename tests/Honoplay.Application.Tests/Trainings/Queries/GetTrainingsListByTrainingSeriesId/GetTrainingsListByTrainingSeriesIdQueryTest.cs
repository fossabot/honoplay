using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Trainings.Queries.GetTrainingsListByTrainingSeriesId;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Queries.GetTrainingsListByTrainingSeriesId
{
    public class GetTrainingsListByTrainingSeriesIdQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTrainingsListByTrainingSeriesIdQueryHandler _getTrainingsListByTrainingSeriesIdQueryHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingSeriesId;

        public GetTrainingsListByTrainingSeriesIdQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingSeriesId);
            _getTrainingsListByTrainingSeriesIdQueryHandler = new GetTrainingsListByTrainingSeriesIdQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int trainingSeriesId)
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
            var trainingCategory = new TrainingCategory
            {
                CreatedBy = adminUser.Id,
                Description = "sample",
                Name = "test"
            };
            context.TrainingCategories.Add(trainingCategory);

            var training = new Training
            {
                CreatedBy = adminUser.Id,
                Description = "description",
                Name = "test",
                EndDateTime = DateTimeOffset.Now.AddDays(5),
                BeginDateTime = DateTimeOffset.Now,
                TrainingSeriesId = trainingSeries.Id,
                TrainingCategoryId = trainingCategory.Id
            };
            context.Trainings.Add(training);

            adminUserId = adminUser.Id;
            trainingSeriesId = trainingSeries.Id;
            tenantId = tenant.Id;

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getTrainingsListByTrainingSeriesIdQuery = new GetTrainingsListByTrainingSeriesIdQuery(_adminUserId, _trainingSeriesId, _tenantId);

            var trainingsResponseModel = await _getTrainingsListByTrainingSeriesIdQueryHandler.Handle(getTrainingsListByTrainingSeriesIdQuery, CancellationToken.None);

            Assert.Null(trainingsResponseModel.Errors);
        }


        public void Dispose() => _context?.Dispose();
    }
}
