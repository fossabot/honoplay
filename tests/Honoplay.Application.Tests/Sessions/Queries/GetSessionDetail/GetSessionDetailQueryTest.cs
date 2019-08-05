using Honoplay.Common._Exceptions;
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

namespace Honoplay.Application.Tests.Sessions.Queries.GetSessionDetail
{
    public class GetSessionDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetSessionDetailQueryHandler _getSessionDetailQueryHandler;
        private readonly int _sessionId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetSessionDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _sessionId, out _tenantId);
            _getSessionDetailQueryHandler = new GetSessionDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int sessionId, out Guid tenantId)
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
                UpdatedBy = adminUser.Id
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

            var trainer = new Trainer
            {
                UpdatedBy = adminUser.Id,
                Name = "sample",
                DepartmentId = department.Id,
                Email = "test@omegabigdata.com",
                PhoneNumber = "16846546544545",
                ProfessionId = profession.Id,
                Surname = "test"
            };
            context.Trainers.Add(trainer);

            var classroom = new Classroom
            {
                UpdatedBy = adminUser.Id,
                Name = "test",
                TrainerId = trainer.Id,
                TrainingId = training.Id,
            };
            context.Classrooms.Add(classroom);

            var game = new Game
            {
                Name = "game1"
            };
            context.Games.Add(game);

            var session = new Session
            {
                UpdatedBy = adminUser.Id,
                Name = "test",
                ClassroomId = classroom.Id,
                GameId = game.Id,
            };
            context.Sessions.Add(session);

            context.SaveChanges();

            tenantId = tenant.Id;
            sessionId = session.Id;
            adminUserId = adminUser.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetSessionDetailQuery(_adminUserId, _sessionId, _tenantId);

            var model = await _getSessionDetailQueryHandler.Handle(query, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.Sessions.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetSessionDetailQuery(_adminUserId, _sessionId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getSessionDetailQueryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
