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

namespace Honoplay.Application.Tests.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateAnswerCommandHandler _updateAnswerCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly int _answerId;
        private readonly Guid _tenantId;

        public UpdateAnswerCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId, out _answerId);
            _updateAnswerCommandHandler = new UpdateAnswerCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int questionId, out Guid tenantId, out int answerId)
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
                UpdatedBy = adminUser.Id
            });

            var question = new Question
            {
                TenantId = tenant.Id,
                Duration = 10,
                Text = "adqwd",
                UpdatedBy = adminUser.Id,
            };
            context.Questions.Add(question);

            var answer = new Answer
            {
                CreatedBy = adminUser.Id,
                OrderBy = 2,
                QuestionId = question.Id,
                Text = "Answer1",
            };
            _context.Answers.Add(answer);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionId = question.Id;
            tenantId = tenant.Id;
            answerId = answer.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateAnswer = new UpdateAnswerCommand
            {
                Id = _answerId,
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                QuestionId = _questionId,
                Text = "answer1",
                OrderBy = 1
            };

            var answerModel = await _updateAnswerCommandHandler.Handle(updateAnswer, CancellationToken.None);

            Assert.Null(answerModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
