using Honoplay.Application.TraineeUsers.Commands.UpdateTraineeUser;
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

namespace Honoplay.Application.Tests.TraineeUsers.Commands.UpdateTraineeUser
{
    public class UpdateTraineeUserCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTraineeUserCommandHandler _commandHandler;
        private readonly int _workingStatusId;
        private readonly int _departmentId;
        private readonly int _adminUserId;
        private readonly int _traineeUserId;
        private readonly Guid _tenantId;

        public UpdateTraineeUserCommandTest()
        {
            var distributedCacheMock = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _workingStatusId, out _departmentId, out _adminUserId, out _traineeUserId, out _tenantId);
            _commandHandler = new UpdateTraineeUserCommandHandler(_context, new CacheManager(distributedCacheMock.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int workingStatusId, out int departmentId, out int adminUserId, out int traineeUserId, out Guid tenantId)
        {
            var context = GetDbContext();

            var salt = ByteArrayExtensions.GetRandomSalt();
            var adminUser = new AdminUser
            {
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

            var department = new Department
            {
                CreatedBy = adminUser.Id,
                Name = "testDepartment",
                TenantId = tenant.Id
            };

            context.Departments.Add(department);

            var workingStatus = new WorkingStatus
            {
                Name = "testWorkingStatus",
                TenantId = tenant.Id,
                CreatedBy = adminUser.Id
            };

            context.WorkingStatuses.Add(workingStatus);

            var traineeUser = new TraineeUser
            {
                Name = "testName",
                DepartmentId = department.Id,
                Surname = "testSurname",
                PhoneNumber = "testNumber11111",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                CreatedBy = adminUser.Id,
                Email = "test@test.com",
                Gender = 1,
                NationalIdentityNumber = "testNumber22222",
                WorkingStatusId = workingStatus.Id
            };
            context.TraineeUsers.Add(traineeUser);

            context.SaveChanges();

            workingStatusId = workingStatus.Id;
            departmentId = department.Id;
            adminUserId = adminUser.Id;
            traineeUserId = traineeUser.Id;
            tenantId = tenant.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateTraineeUserCommand = new UpdateTraineeUserCommand
            {
                Id = _traineeUserId,
                DepartmentId = _departmentId,
                UpdatedBy = _adminUserId,
                Name = "testTraineeUser",
                Gender = 1,
                Password = "testPass1*",
                Email = "asd@gmail.com",
                NationalIdentityNumber = "1231231231231",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                WorkingStatusId = _workingStatusId,
                TenantId = _tenantId
            };

            var traineeUserModel = await _commandHandler.Handle(updateTraineeUserCommand, CancellationToken.None);

            Assert.Null(traineeUserModel.Errors);
            Assert.Equal(expected: updateTraineeUserCommand.Name, actual: traineeUserModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateTraineeUserCommand = new UpdateTraineeUserCommand()
            {
                Id = _traineeUserId + 1,
                DepartmentId = _departmentId,
                UpdatedBy = _adminUserId,
                Name = "testTraineeUser",
                Gender = 1,
                NationalIdentityNumber = "1231231231231",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                WorkingStatusId = _workingStatusId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateTraineeUserCommand, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
