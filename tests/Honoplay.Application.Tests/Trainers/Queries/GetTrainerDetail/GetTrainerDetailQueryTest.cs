﻿using Honoplay.Application.Trainers.Queries.GetTrainerDetail;
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

namespace Honoplay.Application.Tests.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTrainerDetailQueryHandler _queryHandler;
        private readonly int _trainerId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetTrainerDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _trainerId, out _tenantId);
            _queryHandler = new GetTrainerDetailQueryHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int trainerId, out Guid tenantId)
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

            var department = new Department
            {
                CreatedBy = adminUser.Id,
                Name = "testDepartment",
                TenantId = tenant.Id
            };

            context.Departments.Add(department);
            var profession = new Profession
            {
                Name = "testProfession",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            };

            context.Professions.Add(profession);

            var trainer = new Trainer
            {
                Name = "testName",
                DepartmentId = department.Id,
                Surname = "testSurname",
                PhoneNumber = "testNumber11111",
                CreatedBy = adminUser.Id,
                Email = "test@test.com",
                ProfessionId = profession.Id
            };
            context.Trainers.Add(trainer);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            trainerId = trainer.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetTrainerDetailQuery(_adminUserId, _trainerId, _tenantId);

            var model = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.Trainers.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetTrainerDetailQuery(_adminUserId, _trainerId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _queryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
