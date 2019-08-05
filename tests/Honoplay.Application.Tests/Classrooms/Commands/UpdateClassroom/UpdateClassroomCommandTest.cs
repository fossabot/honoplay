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
using Honoplay.Application.Classrooms.Commands.UpdateClassroom;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateClassroomCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingId;
        private readonly int _trainerId;
        private readonly int _classroomId;

        public UpdateClassroomCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingId, out _trainerId, out _classroomId);
            _commandHandler = new UpdateClassroomCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int trainingId, out int trainerId, out int classroomId)
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
                CreatedBy = adminUser.Id,
                Name = "test",
                TrainerId = trainer.Id,
                TrainingId = training.Id,
            };
            context.Classrooms.Add(classroom);
            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            trainingId = training.Id;
            trainerId = trainer.Id;
            classroomId = classroom.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateClassroomCommand = new UpdateClassroomCommand
            {
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                Id = _classroomId,
                TrainerId = _trainerId,
                TrainingId = _trainingId,
                Name = "test"
            };

            var classroomResponseModel = await _commandHandler.Handle(updateClassroomCommand, CancellationToken.None);

            Assert.Null(classroomResponseModel.Errors);
            Assert.Equal(expected: updateClassroomCommand.Name, actual: classroomResponseModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateClassroomCommand = new UpdateClassroomCommand
            {
                Id = _classroomId + 1,
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                TrainerId = _trainerId,
                TrainingId = _trainingId,
                Name = "test"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateClassroomCommand, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
