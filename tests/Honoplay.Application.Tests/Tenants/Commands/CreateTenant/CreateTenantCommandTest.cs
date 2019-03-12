﻿using Honoplay.Application.Tenants.Commands.CreateTenant;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Exceptions;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateTenantCommandHandler _commandHandler;
        private readonly Guid _testTenantGuid;

        public CreateTenantCommandTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _commandHandler = new CreateTenantCommandHandler(_context);
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
            var command = new CreateTenantCommand
            {
                Name = "testto1",
                HostName = "testto1",
                Description = "test desc",
                CreatedBy = 1,
                Logo = new byte[] { 1 }
            };

            var tenantModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(tenantModel.Errors);

            Assert.Equal(command.Name, tenantModel.Items.Single().Name, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new CreateTenantCommand
            {
                Name = "TestTenant#01",
                HostName = "test 1",
                Description = "test desc",
                CreatedBy = 1,
                Logo = new byte[] {1}
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