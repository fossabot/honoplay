﻿using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common._Exceptions;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTenantsListQueryHandler _QueryHandler;
        private readonly Guid _testTenantGuid;
        private readonly int _adminUserId;

        public GetTenantsListQueryTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid, out _adminUserId);
            _QueryHandler = new GetTenantsListQueryHandler(_context);
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantGuid, out int adminUserId)
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

            adminUserId = adminUser.Id;

            var tenant = new Tenant
            {
                Name = "TestTenant#01",
                HostName = "localhost",
                CreatedBy = adminUserId,
            };
            context.Tenants.Add(tenant);

            tenantGuid = tenant.Id;

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUserId,
                CreatedBy = adminUserId,
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetTenantsListQuery(_adminUserId);

            var tenantModel = await _QueryHandler.Handle(query, CancellationToken.None);

            Assert.Null(tenantModel.Errors);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var query = new GetTenantsListQuery(111, 10, 10);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
           await _QueryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose()
        {
            if (_context is null)
            {
                return;
            }
            _context.Dispose();
        }
    }
}