using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common._Exceptions;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTenantDetailQueryHandler _QueryHandler;
        private readonly Guid _testTenantGuid;
        private readonly int _adminUserId;

        public GetTenantDetailQueryTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid, out _adminUserId);
            _QueryHandler = new GetTenantDetailQueryHandler(_context);
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
            var query = new GetTenantDetailQuery(_adminUserId, _testTenantGuid);

            var tenantModel = await _QueryHandler.Handle(query, CancellationToken.None);

            Assert.Null(tenantModel.Errors);
            Assert.Equal(_context.Tenants.FirstOrDefault()?.Name, tenantModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var query = new GetTenantDetailQuery(_adminUserId, Guid.NewGuid());
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