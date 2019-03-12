using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Exceptions;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTenantsListQueryHandler _QueryHandler;
        private readonly Guid _testTenantGuid;

        public GetTenantsListQueryTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _QueryHandler = new GetTenantsListQueryHandler(_context);
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantGuid)
        {
            var context = GetDbContext();
            tenantGuid = Guid.NewGuid();
            context.Tenants.Add(new Tenant
            {
                Id = _testTenantGuid,
                Name = "TestTenant#01",
                HostName = "test 1"
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetTenantsListQuery();

            var tenantModel = await _QueryHandler.Handle(query, CancellationToken.None);

            Assert.Null(tenantModel.Errors);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var query = new GetTenantsListQuery(10, 10);
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