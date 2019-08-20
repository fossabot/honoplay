using Honoplay.Application.Classrooms.Commands.CreateClassroom;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateClassroomCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _trainingId;
        private readonly int _trainerId;
        private readonly int _traineeId;

        public CreateClassroomCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _trainingId, out _trainerId, out _traineeId);
            _commandHandler = new CreateClassroomCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int trainingId, out int trainerId, out int traineeId)
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
                Description = "test",
                Name = "sample",
            };
            context.TrainingCategories.Add(trainingCategory);

            var training = new Training
            {
                CreatedBy = adminUser.Id,
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
                CreatedBy = adminUser.Id,
                Name = "sampleDepartment"
            };
            context.Departments.Add(department);

            var profession = new Profession
            {
                TenantId = tenant.Id,
                CreatedBy = adminUser.Id,
                Name = "testProfession"
            };
            context.Professions.Add(profession);

            var workingStatus = new WorkingStatus
            {
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id,
                Name = "test"
            };
            context.WorkingStatuses.Add(workingStatus);

            var trainee = new Trainee
            {
                CreatedBy = adminUser.Id,
                DepartmentId = department.Id,
                Gender = 1,
                Name = "asdasd",
                NationalIdentityNumber = "12321421311",
                PhoneNumber = "912489213123",
                Surname = "test",
                WorkingStatusId = 1
            };
            context.Trainees.Add(trainee);

            var trainer = new Trainer
            {
                CreatedBy = adminUser.Id,
                Name = "sample",
                DepartmentId = department.Id,
                Email = "test@omegabigdata.com",
                PhoneNumber = "16846546544545",
                ProfessionId = profession.Id,
                Surname = "test"
            };
            context.Trainers.Add(trainer);
            context.SaveChanges();

            adminUserId = adminUser.Id;
            traineeId = trainee.Id;
            tenantId = tenant.Id;
            trainingId = training.Id;
            trainerId = trainer.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new CreateClassroomCommand
            {
                CreatedBy = _adminUserId,
                TenantId = _tenantId,
                CreateClassroomModels = new List<CreateClassroomCommandModel>
                {
                    new CreateClassroomCommandModel
                    {
                        TraineesId = new List<int>{_traineeId},
                        TrainerId = _trainerId,
                        TrainingId = _trainingId,
                        Name = "testasdad"
                    }
                }
            };

            var classroomResponseModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(classroomResponseModel.Errors);

            Assert.True(classroomResponseModel.Items.Single().Count > 0);
        }

        public void Dispose() => _context?.Dispose();
    }
}
