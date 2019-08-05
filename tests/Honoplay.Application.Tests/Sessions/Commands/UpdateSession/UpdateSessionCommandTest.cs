using Honoplay.Application.Sessions.Commands.UpdateSession;
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

namespace Honoplay.Application.Tests.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateSessionCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _gameId;
        private readonly int _classroomId;
        private readonly int _sessionId;

        public UpdateSessionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _gameId, out _classroomId, out _sessionId);
            _commandHandler = new UpdateSessionCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int gameId, out int classroomId, out int sessionId)
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

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            gameId = game.Id;
            sessionId = session.Id;
            classroomId = classroom.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateSessionCommand = new UpdateSessionCommand
            {
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                Id = _sessionId,
                GameId = _gameId,
                ClassroomId = _classroomId,
                Name = "test"
            };

            var sessionResponseModel = await _commandHandler.Handle(updateSessionCommand, CancellationToken.None);

            Assert.Null(sessionResponseModel.Errors);
            Assert.Equal(expected: updateSessionCommand.Name, actual: sessionResponseModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateSessionCommand = new UpdateSessionCommand
            {
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                Id = _sessionId + 1,
                GameId = _gameId,
                ClassroomId = _classroomId,
                Name = "test"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateSessionCommand, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
