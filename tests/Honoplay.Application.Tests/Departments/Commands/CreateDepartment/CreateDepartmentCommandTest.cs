using Honoplay.Application.Departments.Commands.CreateDepartment;
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

namespace Honoplay.Application.Tests.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandTest : TestBase, IDisposable
    {

        private readonly HonoplayDbContext _context;
        private readonly CreateDepartmentCommandHandler _commandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public CreateDepartmentCommandTest()
        {
            var cache = new Mock<IDistributedCache>();

            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _commandHandler = new CreateDepartmentCommandHandler(_context, new CacheManager(cache.Object));
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
            var command = new CreateDepartmentCommand
            {
                AdminUserId = _adminUserId,
                TenantId = _tenantId,
                Departments = new List<string>
                {
                    "a",
                    "b"
                }
            };

            var tenantModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Null(tenantModel.Errors);

            Assert.True(tenantModel.Items.Single().Departments.Count > 0);
        }

        //[Fact]
        //public async Task ShouldThrowErrorWhenInvalidInformation()
        //{
        //    var command = new CreateDepartmentCommand
        //    {
        //        AdminUserId = _adminUserId + 1,
        //        TenantId = new Guid(),
        //        Departments = new List<string>
        //        {
        //            "a",
        //            "b"
        //        }
        //    };

        //    await Assert.ThrowsAsync<NotFoundException>(async () =>
        //   await _commandHandler.Handle(command, CancellationToken.None));
        //}

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
