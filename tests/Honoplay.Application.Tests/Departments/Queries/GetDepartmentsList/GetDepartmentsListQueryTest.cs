﻿using Honoplay.Application.Departments.Queries.GetDepartmentsList;
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

namespace Honoplay.Application.Tests.Departments.Queries.GetDepartmentsList
{
    public class GetDepartmentsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetDepartmentsListQueryHandler _queryHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetDepartmentsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _queryHandler = new GetDepartmentsListQueryHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId)
        {
            var context = GetDbContext();
            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Id = 1,
                Email = "test@omegabigdata.com",
                Password = "pass".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTimeOffset.Now.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);

            adminUserId = adminUser.Id;

            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUserId
            };
            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUserId,
                CreatedBy = adminUserId
            });

            context.Departments.Add(new Department
            {
                Name = "testDepartment",
                CreatedBy = adminUserId,
                TenantId = tenant.Id
            });

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetDepartmentsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var departmentModel = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(departmentModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var query = new GetDepartmentsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var departmentModel = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Single(departmentModel.Items);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
