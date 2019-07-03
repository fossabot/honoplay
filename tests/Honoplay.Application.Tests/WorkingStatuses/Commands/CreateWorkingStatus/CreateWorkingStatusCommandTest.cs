﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateWorkingStatusCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public CreateWorkingStatusCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _commandHandler = new CreateWorkingStatusCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId)
        {
            var context = GetDbContext();

            var salt = ByteArrayExtensions.GetRandomSalt();
            var adminUser = new AdminUser
            {
                Email = "testAdmin@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);

            var tenant = new Tenant
            {
                Name = "TestTenant",
                HostName = "localhost"
            };

            context.Tenants.Add(tenant);
            context.SaveChanges();

            tenantId = tenant.Id;
            adminUserId = adminUser.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createWorkingStatusCommand = new CreateWorkingStatusCommand
            {
                TenantId = _tenantId,
                Name = "workingStatusName",
                CreatedBy = _adminUserId
            };

            var workingStatusModel = await _commandHandler.Handle(createWorkingStatusCommand, CancellationToken.None);

            Assert.Null(workingStatusModel.Errors);
            Assert.Equal(expected: createWorkingStatusCommand.Name,
                         actual: workingStatusModel.Items.Single().Name,
                         ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var createWorkingStatusCommand = new CreateWorkingStatusCommand
            {
                Name = "testWorkingStatusName",
                TenantId = _tenantId,
                CreatedBy = _adminUserId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(createWorkingStatusCommand,
                    CancellationToken.None));

        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
