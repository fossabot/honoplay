using Honoplay.Application.TrainerUsers.Commands.UpdateTrainerUser;
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

namespace Honoplay.Application.Tests.TrainerUsers.Commands.UpdateTrainerUser
{
    public class UpdateTrainerUserCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTrainerUserCommandHandler _commandHandler;
        private readonly int _professionId;
        private readonly int _departmentId;
        private readonly int _adminUserId;
        private readonly int _trainerUserId;
        private readonly Guid _tenantId;

        public UpdateTrainerUserCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _professionId, out _departmentId, out _adminUserId, out _trainerUserId, out _tenantId);
            _commandHandler = new UpdateTrainerUserCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int professionId, out int departmentId, out int adminUserId, out int trainerUserId, out Guid tenantId)
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

            var trainerUser = new TrainerUser
            {
                Name = "testName",
                DepartmentId = department.Id,
                Surname = "testSurname",
                PhoneNumber = "testNumber11111",
                CreatedBy = adminUser.Id,
                Email = "test@test.com",
                ProfessionId = profession.Id
            };
            context.TrainerUsers.Add(trainerUser);

            context.SaveChanges();

            professionId = profession.Id;
            departmentId = department.Id;
            adminUserId = adminUser.Id;
            trainerUserId = trainerUser.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateTrainerUserCommand = new UpdateTrainerUserCommand()
            {
                Id = _trainerUserId,
                DepartmentId = _departmentId,
                UpdatedBy = _adminUserId,
                Name = "testTraineasdasdr",
                Surname = "qwdqwdqasdwd",
                PhoneNumber = "123123123123",
                ProfessionId = _professionId,
                Email = "test@test.com",
                TenantId = _tenantId
            };

            var trainerUserModel = await _commandHandler.Handle(updateTrainerUserCommand, CancellationToken.None);

            Assert.Null(trainerUserModel.Errors);
            Assert.Equal(expected: updateTrainerUserCommand.Name, actual: trainerUserModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var updateTrainerUserCommand = new UpdateTrainerUserCommand()
            {
                Id = _trainerUserId + 1,
                DepartmentId = _departmentId,
                UpdatedBy = _adminUserId,
                Name = "testTrainerUser",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                ProfessionId = _professionId,
                TenantId = _tenantId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(updateTrainerUserCommand, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
