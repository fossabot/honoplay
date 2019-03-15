using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using Honoplay.Application._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTenantCommandHandler _commandHandler;
        private readonly Guid _testTenantGuid;

        public UpdateTenantCommandTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _commandHandler = new UpdateTenantCommandHandler(_context);
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantGuid)
        {
            var context = GetDbContext();
            tenantGuid = Guid.NewGuid();

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

            context.Tenants.Add(new Tenant
            {
                Id = _testTenantGuid,
                Name = "TestTenant#01",
                HostName = "test 1"
            });

            context.Tenants.Add(new Tenant
            {
                Name = "TestTenant#02",
                HostName = "test 2"
            });

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = _testTenantGuid,
                AdminUserId = 1,
                CreatedBy = 1
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new UpdateTenantCommand
            {
                Id = _testTenantGuid,
                Name = "testto1",
                HostName = "testto1",
                Description = "test desc",
                UpdatedBy = 1,
                Logo = new byte[] { 1 }
            };

            var tenantModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(tenantModel.Errors);

            Assert.Equal(command.Name, tenantModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new UpdateTenantCommand
            {
                Id = _testTenantGuid,
                Name = "TestTenant#02",
                HostName = "test 2",
                Description = "test desc",
                UpdatedBy = 1,
                Logo = new byte[] { 1 }
            };

            await Assert.ThrowsAsync<ObjectAlreadyExistsException>(async () =>
           await _commandHandler.Handle(command, CancellationToken.None));
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