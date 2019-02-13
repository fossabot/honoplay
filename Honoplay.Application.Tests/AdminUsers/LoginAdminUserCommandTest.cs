using Honoplay.Application.Tokens.Commands;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Honoplay.Application.Exceptions;

namespace Honoplay.Application.Tests.Tokens
{
    public class LoginAdminUserCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly LoginAdminUserCommandHandler _commandHandler;
        private readonly Guid _testTenantGuid;
        public LoginAdminUserCommandTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _commandHandler = new LoginAdminUserCommandHandler(_context);
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantGuid)
        {
            var context = GetDbContext();
            tenantGuid = Guid.NewGuid();
            context.Tenants.Add(new Tenant
            {
                Id = _testTenantGuid,
                Name = "TestTenant#01"
            });

            context.AdminUsers.Add(new AdminUser
            {
                TenantId = _testTenantGuid,
                Username = "TestAdminUser#01",
                //Omega
                Password = new byte[] { 213, 15, 188, 206, 153, 60, 130, 254, 153, 23, 161, 161, 34, 250, 45, 174, 50, 172, 195, 94, 195, 228, 219, 196, 69, 251, 105, 138, 223, 138, 3, 6, 245, 214, 235, 110, 29, 104, 11, 225, 234, 191, 62, 51, 93, 122, 42, 109, 154, 103, 77, 8, 179, 143, 7, 107, 23, 216, 76, 29, 181, 172, 193, 113 },
                PasswordSalt = new byte[] { 123, 123, 123 },
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
                CreatedBy = null,
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var command = new LoginAdminUserCommand
            {
                TenantId = _testTenantGuid,
                Username = "TestAdminUser#01",
                Password = "Omega"
            };

            var adminUser = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(adminUser);
            Assert.Equal(command.Username, adminUser.Username, ignoreCase: true);
            Assert.Equal(command.TenantId, adminUser.TenantId);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new LoginAdminUserCommand
            {
                TenantId = _testTenantGuid,
                Username = "TestAdminUser#01",
                Password = "Omega2"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
           await _commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowErrorWhenCaseErrorInPassword()
        {
            var command = new LoginAdminUserCommand
            {
                TenantId = _testTenantGuid,
                Username = "TestAdminUser#01",
                Password = "omega"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
            await _commandHandler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task ShouldThrowErrorWhenCaseErrorInUsername()
        {
            var command = new LoginAdminUserCommand
            {
                TenantId = _testTenantGuid,
                Username = "testAdminUser#01",
                Password = "Omega"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                   await _commandHandler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task ShouldIncreaseInvalidPasswordAttempsNumberWhenInvalidInformation()
        {
            var command = new LoginAdminUserCommand
            {
                TenantId = _testTenantGuid,
                Username = "TestAdminUser#01",
                Password = "Omega2"
            };

            var dbUser = _context
                            .AdminUsers
                            .First(u => u.TenantId == _testTenantGuid
                                    && u.Username == command.Username);

            var before = dbUser.NumberOfInvalidPasswordAttemps;

            await Assert.ThrowsAsync<Exception>(async () =>
            await _commandHandler.Handle(command, CancellationToken.None));
            var current = dbUser.NumberOfInvalidPasswordAttemps;
            var actual = current - before;
            Assert.Equal(1,actual);
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
