﻿using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Commands.CreateTrainingSeries
{
    public class CreateTrainingSeriesCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateTrainingSeriesCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public CreateTrainingSeriesCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _commandHandler = new CreateTrainingSeriesCommandHandler(_context, new CacheManager(cache.Object));
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
            var command = new CreateTrainingSeriesCommand
            {
                CreatedBy = _adminUserId,
                TenantId = _tenantId,
                Name = "testTraining"
            };

            var trainingSeriesResponseModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(trainingSeriesResponseModel.Errors);

            Assert.True(trainingSeriesResponseModel.Items.Count > 0);
        }

        public void Dispose() => _context?.Dispose();
    }
}
