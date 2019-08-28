﻿using Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList;
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

namespace Honoplay.Application.Tests.TraineeUsers.Queries.GetTraineeUsersList
{
    public class GetTraineeUsersListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTraineeUsersListQueryHandler _queryHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public GetTraineeUsersListQueryTest()
        {
            var distributedCacheMock = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId);
            _queryHandler = new GetTraineeUsersListQueryHandler(_context, new CacheManager(distributedCacheMock.Object));
        }


        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId)
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
                CreatedBy = adminUser.Id,
                Gender = 1,
                NationalIdentityNumber = "testNumber22222",
                WorkingStatusId = workingStatus.Id
            };
            context.TraineeUsers.Add(traineeUser);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var traineeUsersListQuery = new GetTraineeUsersListQuery(adminUserId: _adminUserId, tenantId: _tenantId, skip: 0, take: 11);

            var model = await _queryHandler.Handle(traineeUsersListQuery, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.TraineeUsers.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }


        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var traineeUsersListQuery = new GetTraineeUsersListQuery(adminUserId: _adminUserId, tenantId: _tenantId, skip: 0, take: 0);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _queryHandler.Handle(traineeUsersListQuery, CancellationToken.None));
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
