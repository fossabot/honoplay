using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Professions.Commands.CreateProfession
{
    public class CreateProfessionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateProfessionCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public CreateProfessionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _commandHandler = new CreateProfessionCommandTest(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId)
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

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;

            return context;
        }
        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new CreateProfessionCommand
            {
                AdminUserId = _adminUserId,
                TenantId = _tenantId,
                Professions = new List<string>
                {
                    "a",
                    "b"
                }
            };

            var professionsResponseModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(professionsResponseModel.Errors);

            Assert.True(professionsResponseModel.Items.Single().Professions.Count > 0);
        }
        public void Dispose()
        {
        }
    }
}
