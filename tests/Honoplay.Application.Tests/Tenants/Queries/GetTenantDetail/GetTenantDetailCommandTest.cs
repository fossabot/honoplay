using Honoplay.Application.Exceptions;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTenantDetailQueryHandler _QueryHandler;
        private readonly Guid _testTenantGuid;

        public GetTenantDetailQueryTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _QueryHandler = new GetTenantDetailQueryHandler(_context);
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
            var query = new GetTenantDetailQuery(_testTenantGuid);

            var tenantModel = await _QueryHandler.Handle(query, CancellationToken.None);

            Assert.Null(tenantModel.Errors);
            Assert.Equal(_context.Tenants.FirstOrDefault().Name, tenantModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var query = new GetTenantDetailQuery(Guid.NewGuid());
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