using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Honoplay.Application.Exceptions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Tokens
{
    public class RegisterAdminUserCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly RegisterAdminUserCommandHandler _commandHandler;

        public RegisterAdminUserCommandTest()
        {
            _context = InitAndGetDbContext();
            _commandHandler = new RegisterAdminUserCommandHandler(_context);
        }

        private HonoplayDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();
            return context;
        }

        [Fact]
        public async Task ShouldRegisterWithoutErrorAndWithExistingError()
        {
            var command = new RegisterAdminUserCommand
            {
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Omega",
                Name = "Test",
                Surname = "Ex",
                TimeZone = 0
            };

            var adminUser = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(adminUser.CreatedDateTime);
            Assert.Equal(adminUser.CreatedDateTime.Value.Date, DateTime.Today);
            Assert.NotNull(adminUser.Password);
            Assert.Equal(command.Email, adminUser.Email, ignoreCase: true);
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