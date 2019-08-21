using Honoplay.Application.Sessions.Queries.GetSessionsListByClassroomId;
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

namespace Honoplay.Application.Tests.Options.Queries.GetSessionsListByClassroomId
{
    public class GetSessionsListByClassroomIdQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetSessionsListByClassroomIdQueryHandler _getSessionsListByClassroomIdQueryHandler;
        private readonly int _classroomId;
        private readonly Guid _tenantId;

        public GetSessionsListByClassroomIdQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _classroomId);
            _getSessionsListByClassroomIdQueryHandler = new GetSessionsListByClassroomIdQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int classroomId)
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
            classroomId = classroom.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var optionsListByQuestionIdQuery = new GetSessionsListByClassroomIdQuery(_classroomId, _tenantId);

            var optionsListByQuestionIdResponseModel = await _getSessionsListByClassroomIdQueryHandler.Handle(optionsListByQuestionIdQuery, CancellationToken.None);

            Assert.Null(optionsListByQuestionIdResponseModel.Errors);
            Assert.Equal(expected: _context.Sessions.First().Name, actual: optionsListByQuestionIdResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var optionsListByQuestionIdQuery = new GetSessionsListByClassroomIdQuery(_classroomId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getSessionsListByClassroomIdQueryHandler.Handle(optionsListByQuestionIdQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
