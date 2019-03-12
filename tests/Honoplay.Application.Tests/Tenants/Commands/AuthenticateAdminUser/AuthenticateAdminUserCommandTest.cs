using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.AuthenticateAdminUser
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

            var salt = ByteArrayExtensions.GetRandomSalt();
            context.AdminUsers.Add(new AdminUser
            {
                Email = "TestAdminUser01@omegabigdata.com",
                //Omega
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
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
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Passw0rd"
            };

            var adminUser = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(command.Email, adminUser.Email, ignoreCase: true);
        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var command = new AuthenticateAdminUserCommand
            {
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Passw0rdEX"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
           await _commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldThrowErrorWhenCaseErrorInPassword()
        {
            var command = new AuthenticateAdminUserCommand
            {
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "passw0rd"
            };

            await Assert.ThrowsAsync<Exception>(async () =>
            await _commandHandler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ShouldIncreaseInvalidPasswordAttempsNumberWhenInvalidInformation()
        {
            var command = new AuthenticateAdminUserCommand
            {
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Passw0rd2"
            };

            var dbUser = _context
                            .AdminUsers
                            .First(u => u.Email == command.Email);

            var before = dbUser.NumberOfInvalidPasswordAttemps;

            await Assert.ThrowsAsync<Exception>(async () =>
            await _commandHandler.Handle(command, CancellationToken.None));
            var current = dbUser.NumberOfInvalidPasswordAttemps;
            var actual = current - before;
            Assert.Equal(1, actual);
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