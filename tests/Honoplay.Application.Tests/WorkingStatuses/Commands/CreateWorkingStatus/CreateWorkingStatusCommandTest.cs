﻿using Honoplay.Application.WorkingStatuses.Commands.CreateWorkingStatus;
using Honoplay.Common._Exceptions;
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

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.CreateWorkingStatus
{
    public class CreateWorkingStatusCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateWorkingStatusCommandHandler _commandHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;
        public CreateWorkingStatusCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId);
            _commandHandler = new CreateWorkingStatusCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId)
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
