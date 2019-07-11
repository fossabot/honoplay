using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTenantsListQueryHandler _queryHandler;
        private readonly Guid _testTenantGuid;
        private readonly int _adminUserId;

        public GetTenantsListQueryTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid, out _adminUserId);
            _queryHandler = new GetTenantsListQueryHandler(_context);
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
            var query = new GetTenantsListQuery(tenantId: _testTenantGuid, null, null);

            var tenantModel = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(tenantModel.Errors);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var query = new GetTenantsListQuery(tenantId: Guid.NewGuid(), skip: -3, take: 10);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
           await _queryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}