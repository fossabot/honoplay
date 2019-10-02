using Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTraineeGroupCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _traineeGroupId;

        public UpdateTraineeGroupCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _traineeGroupId);
            _commandHandler = new UpdateTraineeGroupCommandHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int traineeGroupId)
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
                UpdatedBy = adminUser.Id
            });

            var traineeGroup = new TraineeGroup
            {
                TenantId = tenant.Id,
                Name = "Test1asd",
                UpdatedBy = adminUser.Id
            };
            context.Add(traineeGroup);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            traineeGroupId = traineeGroup.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new UpdateTraineeGroupCommand
            {
                
                UpdatedBy = _adminUserId,
                TenantId = _tenantId,
                UpdateTraineeGroupCommandModels = new List<UpdateTraineeGroupCommandModel>
                {
                    new UpdateTraineeGroupCommandModel
                    {
                        Name = "Updated"

                    }
                }
            };

            var traineeGroupModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(traineeGroupModel.Errors);

            Assert.True(traineeGroupModel.Items.Single().Count > 0);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
