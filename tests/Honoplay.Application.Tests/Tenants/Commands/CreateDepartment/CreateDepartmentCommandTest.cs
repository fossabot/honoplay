using Honoplay.Application._Exceptions;
using Honoplay.Application.Tenants.Commands.AddDepartment;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.CreateDepartment
{
    public class CreateDepartmentCommandTest : TestBase, IDisposable
    {


        private readonly HonoplayDbContext _context;
        private readonly CreateDepartmentCommandHandler _commandHandler;
        private readonly string _hostName;
        private readonly int _adminUserId;

        public CreateDepartmentCommandTest()
        {
            _context = InitAndGetDbContext(out _hostName, out _adminUserId);
            _commandHandler = new CreateDepartmentCommandHandler(_context);
        }

        private HonoplayDbContext InitAndGetDbContext(out string hostName, out int adminUserId)
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
                HostName = "test 1"
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
            hostName = tenant.HostName;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new CreateDepartmentCommand
            {
                AdminUserId = _adminUserId,
                HostName = _hostName,
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

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new CreateDepartmentCommand
            {
                AdminUserId = _adminUserId + 1,
                HostName = _hostName,
                Departments = new List<string>
                {
                    "a",
                    "b"
                }
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
           await _commandHandler.Handle(command, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
