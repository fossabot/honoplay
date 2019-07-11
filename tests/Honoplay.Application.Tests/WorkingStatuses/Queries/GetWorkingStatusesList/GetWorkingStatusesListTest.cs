using Honoplay.Application.WorkingStatuses.Queries;
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

namespace Honoplay.Application.Tests.WorkingStatuses.Queries.GetWorkingStatusesList
{
    public class GetWorkingStatusesListTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetWorkingStatusesListQueryHandler _queryHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetWorkingStatusesListTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId);
            _queryHandler = new GetWorkingStatusesListQueryHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId)
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

            context.WorkingStatuses.Add(new WorkingStatus
            {
                Name = "testStatus",
                TenantId = tenant.Id,
                CreatedBy = adminUserId
            });

            context.SaveChanges();
            tenantId = tenant.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var workingStatusesListQuery = new GetWorkingStatusesListQuery(adminUserId: _adminUserId, tenantId: _tenantId, skip: 0, take: 10);

            var workingStatusesListModel = await _queryHandler.Handle(workingStatusesListQuery, CancellationToken.None);

            Assert.Null(workingStatusesListModel.Errors);
            Assert.Equal(expected: _context.WorkingStatuses.FirstOrDefault()?.Name, actual: workingStatusesListModel.Items.Single().Name, ignoreCase: true);

        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
