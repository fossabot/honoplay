using Honoplay.Application.Questions.Commands.UpdateQuestion;
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
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public UpdateQuestionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId);
            _updateQuestionCommandHandler = new UpdateQuestionCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int questionId, out Guid tenantId)
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

            var question = new Question
            {
                TenantId = tenant.Id,
                Duration = 10,
                Text = "adqwd",
                CreatedBy = adminUser.Id,
            };
            context.Questions.Add(question);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionId = question.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateQuestion = new UpdateQuestionCommand
            {
                Id = _questionId,
                TenantId = _tenantId,
                UpdatedBy = _adminUserId,
                Text = "Asagidakilerden hangisi asagidadir?",
                Duration = 3
            };

            var questionModel = await _updateQuestionCommandHandler.Handle(updateQuestion, CancellationToken.None);

            Assert.Null(questionModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
