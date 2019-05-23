using Honoplay.Application._Exceptions;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateTrainerCommandHandler _commandHandler;
        private readonly int _professionId;
        private readonly int _departmentId;
        private readonly int _adminUserId;

        public CreateTrainerCommandTest()
        {
            _context = InitAndGetDbContext(out _professionId, out _departmentId, out _adminUserId);
            _commandHandler = new CreateTrainerCommandHandler(_context);
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int professionId, out int departmentId, out int adminUserId)
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
                HostName = "test 1"
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
                CreatedBy = adminUser.Id
            };

            context.Professions.Add(profession);

            context.SaveChanges();

            professionId = profession.Id;
            departmentId = department.Id;
            adminUserId = adminUser.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createTrainerCommand = new CreateTrainerCommand()
            {
                DepartmentId = _departmentId,
                CreatedBy = _adminUserId,
                Name = "testTrainer",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                ProfessionId = _professionId,
                Email = "test123@gmail.com"
            };

            var trainerModel = await _commandHandler.Handle(createTrainerCommand, CancellationToken.None);

            Assert.Null(trainerModel.Errors);
            Assert.Equal(expected: createTrainerCommand.Name, actual: trainerModel.Items.Single().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInvalidInformation()
        {
            var createTrainerCommand = new CreateTrainerCommand()
            {
                DepartmentId = _departmentId + 1,
                CreatedBy = _adminUserId,
                Name = "testTrainer",
                Surname = "qwdqwdqwd",
                PhoneNumber = "123123123123",
                ProfessionId = _professionId
            };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _commandHandler.Handle(createTrainerCommand, CancellationToken.None));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
