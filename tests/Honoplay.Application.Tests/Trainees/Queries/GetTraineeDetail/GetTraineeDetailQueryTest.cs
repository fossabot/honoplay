using Honoplay.Application._Exceptions;
using Honoplay.Application.Tenants.Queries.GetTraineeDetail;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTraineeDetailQueryHandler _queryHandler;
        private readonly int _traineeId;
        private readonly int _adminUserId;

        public GetTraineeDetailQueryTest()
        {
            _context = InitAndGetDbContext(out _adminUserId, out _traineeId);
            _queryHandler = new GetTraineeDetailQueryHandler(_context);
        }


        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int traineeId)
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

            var workingStatus = new WorkingStatus
            {
                Name = "testWorkingStatus",
            };

            context.WorkingStatuses.Add(workingStatus);

            var trainee = new Trainee
            {
                Name = "testName",
                DepartmentId = department.Id,
                Surname = "testSurname",
                PhoneNumber = "testNumber11111",
                CreatedBy = adminUser.Id,
                Gender = 1,
                NationalIdentityNumber = "testNumber22222",
                WorkingStatusId = workingStatus.Id
            };
            context.Trainees.Add(trainee);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            traineeId = trainee.Id;
            return context;
        }


        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var query = new GetTraineeDetailQuery(_adminUserId, _traineeId);

            var model = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.Null(model.Errors);
            Assert.Equal(expected: _context.Trainees.FirstOrDefault()?.Name, actual: model.Items.Single().Name, ignoreCase: true);

        }


        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetTraineeDetailQuery(_adminUserId, _traineeId+1);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _queryHandler.Handle(query, CancellationToken.None));
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
