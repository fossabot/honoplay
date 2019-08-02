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

namespace Honoplay.Application.Tests.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetClassroomListQueryHandler _getClassroomListQueryHandler;
        private readonly Guid _tenantId;
        public GetClassroomsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId);
            _getClassroomListQueryHandler = new GetClassroomListQueryHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId)
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

            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getClassroomsListQuery = new GetClassroomsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var classroomsResponseModel = await _getClassroomsListQueryHandler.Handle(getClassroomsListQuery, CancellationToken.None);

            Assert.Null(classroomsResponseModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getClassroomsListQuery = new GetClassroomsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var classroomsResponseModel = await _getClassroomsListQueryHandler.Handle(getClassroomsListQuery, CancellationToken.None);

            Assert.Single(classroomsResponseModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
