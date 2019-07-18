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
using Honoplay.Application.Questions.Commands.CreateQuestion;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateQuestionCommandHandler _createQuestionCommandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;

        public CreateQuestionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _createQuestionCommandHandler = new CreateQuestionCommandHandler(_context, new CacheManager(cache.Object));
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
            var createQuestionCommand = new CreateQuestionCommand
            {
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                Text = "Asagidakilerden hangisi asagidadir?",
                Duration = 3
            };

            var questionModel = await _createQuestionCommandHandler.Handle(createQuestionCommand, CancellationToken.None);

            Assert.Null(questionModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
