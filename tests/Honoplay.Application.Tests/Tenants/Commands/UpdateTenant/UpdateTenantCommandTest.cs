using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Exceptions;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
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