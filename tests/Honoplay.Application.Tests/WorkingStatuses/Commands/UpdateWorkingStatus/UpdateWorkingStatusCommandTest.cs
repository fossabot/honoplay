using Honoplay.Common._Exceptions;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Persistence.CacheManager;
using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Commands.UpdateWorkingStatus
{
    public class UpdateWorkingStatusCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly int _adminUserId;
        private readonly int _workingStatusId;
        private readonly UpdateWorkingStatusCommandHandler _commandHandler;

        public UpdateWorkingStatusCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _workingStatusId);
            _commandHandler = new UpdateWorkingStatusCommandHandler(_context, new CacheManager(cache.Object));
        }


        public HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int workingStatusId)
        {
            var context = GetDbContext();

            var salt = ByteArrayExtensions.GetRandomSalt();
            var adminUser = new AdminUser
            {
                Name = "testAdmin",
                Email = "testAdmin@omegabigdata.com",
                Password = "Password01".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);

            var tenant = new Tenant
            {
                Name = "TestTenant",
                HostName = "localhost",
                CreatedBy = adminUser.Id,
            };

            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var workingStatus = new WorkingStatus
            {
                Name = "testWorkingStatus",
                TenantId = tenant.Id,
                CreatedBy = adminUser.Id
            };

            context.SaveChanges();

            adminUserId = adminUser.Id;
            workingStatusId = workingStatus.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateWorkingStatusCommand = new UpdateWorkingStatusCommand
            {
                Id = _workingStatusId,
                HostName = "localhost",
                UpdatedBy = _adminUserId,
                Name = "testWorkingStatusUpdate",
            };

            var workingStatusModel = await _commandHandler.Handle(updateWorkingStatusCommand, CancellationToken.None);

            Assert.Null(workingStatusModel.Errors);
            Assert.Equal(expected: updateWorkingStatusCommand.Name, actual: workingStatusModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateWorkingStatusCommand = new UpdateWorkingStatusCommand
            {
                Id = _workingStatusId + 1,
                HostName = "localhost",
                UpdatedBy = _adminUserId,
                Name = "testWorkingStatusUpdate",
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateWorkingStatusCommand, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
