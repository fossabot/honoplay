using Honoplay.Application.TraineeGroups.Queries.GetTraineeGroupDetail;
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

namespace Honoplay.Application.Tests.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public class GetTraineeGroupDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTraineeGroupDetailQueryHandler _queryHandler;
        private readonly int _traineeGroupId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetTraineeGroupDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _traineeGroupId, out _tenantId);
            _queryHandler = new GetTraineeGroupDetailQueryHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int traineeGroupId, out Guid tenantId)
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
                HostName = "localhost"
            };

            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var traineeGroup = new TraineeGroup
            {
                TenantId = tenant.Id,
                Name = "adqwd",
                CreatedBy = adminUser.Id
            };
            context.TraineeGroups.Add(traineeGroup);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            traineeGroupId = traineeGroup.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetTraineeGroupDetailQuery(_traineeGroupId, _adminUserId, _tenantId);

            var model = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.TraineeGroups.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetTraineeGroupDetailQuery(123 + _traineeGroupId, _adminUserId, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _queryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}