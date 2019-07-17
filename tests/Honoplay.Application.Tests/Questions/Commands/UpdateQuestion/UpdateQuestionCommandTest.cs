using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateQuestionCommandHandler _updateQuestionCommandHandler;
        private readonly Guid _tenantId;
        private readonly int _adminUserId;
        private readonly int _questionId;

        public UpdateQuestionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId, out _questionId);
            _updateQuestionCommandHandler = new UpdateQuestionCommandHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId, out int questionId)
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

            var question = new Question
            {
                TenantId = tenant.Id,
                CreatedBy = adminUser.Id,
                Text = "asdasd",
                Duration = 3
            };
            _context.Questions.Add(question);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            questionId = question.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateQuestion = new UpdateQuestionCommand
            {
                Id = _questionId,
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                Text = "Asagidakilerden hangisi asagidadir?",
                Duration = 3
            };

            var questionModel = await _updateQuestionCommandHandler.Handle(updateQuestion, CancellationToken.None);

            Assert.Null(questionModel.Errors);
            Assert.NotNull(questionModel.Items.Single().CreateQuestionModel);
        }

        public void Dispose() => _context?.Dispose();
    }
}
