using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
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
        private readonly int _adminUserId;
        private const string _hostName = "localhost";
        private readonly GetWorkingStatusesListQueryHandler _queryHandler;

        public GetWorkingStatusesListTest()
        {
            var cache = new Mock<IDistributedCache>();
            _queryHandler = new GetWorkingStatusesListQueryHandler(_context, cache.Object);
        }

        private HonoplayDbContext InitAndGetDbContext()
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
                HostName = _hostName
            };

            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            context.WorkingStatuses.Add(new WorkingStatus
            {
                Name = "testStatus",
                TenantId = tenant.Id
            });

            _context.SaveChanges();

            return _context;

        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetWorkingStatusesListQuery(_adminUserId, _hostName, skip: 0, take: 10);

            var model = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.WorkingStatuses.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetWorkingStatusesListQuery(_adminUserId, _hostName, skip: 0, take: 0);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _queryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
