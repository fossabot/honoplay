using Honoplay.Application.AdminUsers.Commands;
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
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;

namespace Honoplay.Application.Tests.Tokens
{
    public class AuthenticateAdminUserCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly AuthenticateAdminUserCommandHandler _commandHandler;
        private readonly Guid _testTenantGuid;
        public AuthenticateAdminUserCommandTest()
        {
            _context = InitAndGetDbContext(out _testTenantGuid);
            _commandHandler = new AuthenticateAdminUserCommandHandler(_context);
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
                UserName = "TestAdminUser#01",
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
            var command = new AuthenticateAdminUserCommand
            {
                UserName = "TestAdminUser#01",
                Password = "Omega"
            };

            var adminUser = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(adminUser);
            Assert.Equal(command.UserName, adminUser.UserName, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new AuthenticateAdminUserCommand
            {
                UserName = "TestAdminUser#01",
                Password = "Omega2"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
           await _commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowErrorWhenCaseErrorInPassword()
        {
            var command = new AuthenticateAdminUserCommand
            {
                UserName = "TestAdminUser#01",
                Password = "omega"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
            await _commandHandler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task ShouldThrowErrorWhenCaseErrorInUserName()
        {
            var command = new AuthenticateAdminUserCommand
            {
                UserName = "testAdminUser#01",
                Password = "Omega"
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                   await _commandHandler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task ShouldIncreaseInvalidPasswordAttempsNumberWhenInvalidInformation()
        {
            var command = new AuthenticateAdminUserCommand
            {
                UserName = "TestAdminUser#01",
                Password = "Omega2"
            };

            var dbUser = _context
                            .AdminUsers
                            .First(u => u.UserName == command.UserName);

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
