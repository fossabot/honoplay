using Honoplay.Application.Professions.Queries.GetProfessionsList;
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

namespace Honoplay.Application.Tests.Professions.Queries.GetProfessionsList
{
    public class GetProfessionsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetProfessionsListQueryHandler _queryHandler;
        private readonly Guid _tenantId;

        public GetProfessionsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _queryHandler = new GetProfessionsListQueryHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId)
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

            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUser.Id
            };
            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            context.Professions.Add(new Profession
            {
                Name = "testProfession",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            });

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetProfessionsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var professionModel = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(professionModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var query = new GetProfessionsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var professionModel = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Single(professionModel.Items);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
