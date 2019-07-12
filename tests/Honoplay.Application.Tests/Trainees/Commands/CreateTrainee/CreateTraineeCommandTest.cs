﻿using Honoplay.Application.Trainees.Commands.CreateTrainee;
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

namespace Honoplay.Application.Tests.Trainees.Commands.CreateTrainee
{
    public class CreateTraineeCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateTraineeCommandHandler _commandHandler;
        private readonly int _workingStatusId;
        private readonly int _departmentId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateTraineeCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _workingStatusId, out _departmentId, out _adminUserId, out _tenantId);
            _commandHandler = new CreateTraineeCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int workingStatusId, out int departmentId, out int adminUserId, out Guid tenantId)
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

            context.SaveChanges();

            workingStatusId = workingStatus.Id;
            departmentId = department.Id;
            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createTraineeCommand = new CreateTraineeCommand
            {
                DepartmentId = _departmentId,
                CreatedBy = _adminUserId,
                Name = "testTrainee",
                Gender = 1,
                NationalIdentityNumber = "1231231231231",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                WorkingStatusId = _workingStatusId,
                TenantId = _tenantId
            };

            var traineeModel = await _commandHandler.Handle(createTraineeCommand, CancellationToken.None);

            Assert.Null(traineeModel.Errors);
            Assert.Equal(expected: createTraineeCommand.Name, actual: traineeModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var createTraineeCommand = new CreateTraineeCommand()
            {
                DepartmentId = _departmentId + 1,
                CreatedBy = _adminUserId,
                Name = "testTrainee",
                Gender = 1,
                NationalIdentityNumber = "1231231231231",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                WorkingStatusId = _workingStatusId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(createTraineeCommand, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
