using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Classrooms.Queries.GetClassroomsList;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Queries.GetClassroomsList
{
    public class GetClassroomsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetClassroomsListQueryHandler _getClassroomsListQueryHandler;
        private readonly Guid _tenantId;
        public GetClassroomsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId);
            _getClassroomsListQueryHandler = new GetClassroomsListQueryHandler(_context, new CacheManager(cache.Object));
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
                UpdatedBy = adminUser.Id,
                Name = "testSeries"
            };
            context.TrainingSerieses.Add(trainingSeries);

            var trainingCategory = new TrainingCategory
            {
                UpdatedBy = adminUser.Id,
                Description = "test",
                Name = "sample",
            };
            context.TrainingCategories.Add(trainingCategory);

            var training = new Training
            {
                UpdatedBy = adminUser.Id,
                BeginDateTime = DateTimeOffset.Now,
                Description = "test",
                EndDateTime = DateTimeOffset.Now.AddDays(5),
                TrainingCategoryId = trainingCategory.Id,
                TrainingSeriesId = trainingSeries.Id,
                Name = "sample"
            };
            context.Trainings.Add(training);

            var department = new Department
            {
                TenantId = tenant.Id,
                UpdatedBy = adminUser.Id,
                Name = "sampleDepartment"
            };
            context.Departments.Add(department);

            var profession = new Profession
            {
                TenantId = tenant.Id,
                UpdatedBy = adminUser.Id,
                Name = "testProfession"
            };
            context.Professions.Add(profession);

            var trainerUser = new TrainerUser
            {
                UpdatedBy = adminUser.Id,
                Name = "sample",
                DepartmentId = department.Id,
                Email = "test@omegabigdata.com",
                PhoneNumber = "16846546544545",
                ProfessionId = profession.Id,
                Surname = "test"
            };
            context.TrainerUsers.Add(trainerUser);

            var classroom = new Classroom
            {
                CreatedBy = adminUser.Id,
                Name = "test",
                TrainerUserId = trainerUser.Id,
                TrainingId = training.Id,
            };
            context.Classrooms.Add(classroom);
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
