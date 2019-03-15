using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Honoplay.Application._Exceptions;
using Honoplay.Persistence;
using Xunit;

namespace Honoplay.Application.Tests.AdminUsers.Commands.RegisterAdminUser
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
                Password = "Passw0rd",
                Name = "Test",
                Surname = "Ex",
                TimeZone = 0
            };

            var adminUserModel = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal(1, adminUserModel.NumberOfTotalItems);

            var adminUser = adminUserModel.Items.Single();

            Assert.NotNull(adminUser.CreatedDateTime);
            Assert.Equal(adminUser.CreatedDateTime.Value.Date, DateTime.Today);

            var adminUsers = _context.AdminUsers.ToList();
            Assert.NotNull(adminUsers.FirstOrDefault());
            Assert.Single(adminUsers);
            Assert.Equal(command.Email, adminUsers.FirstOrDefault().Email.ToLower(), ignoreCase: true);

            command.Email = "TestAdminUser02@omegabigdata.com";
            await _commandHandler.Handle(command, CancellationToken.None);

            adminUsers = _context.AdminUsers.ToList();
            Assert.Equal(2, adminUsers.Count);

            await Assert.ThrowsAsync<ObjectAlreadyExistsException>(async () => await _commandHandler.Handle(command, CancellationToken.None));
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