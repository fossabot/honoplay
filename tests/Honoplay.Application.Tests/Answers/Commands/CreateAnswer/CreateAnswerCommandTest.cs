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

namespace Honoplay.Application.Tests.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateAnswerCommandHandler _createAnswerCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateAnswerCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId);
            _createAnswerCommandHandler = new CreateAnswerCommandHandler(_context, new CacheManager(cache.Object));
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
            var createAnswer = new CreateAnswerCommand
            {
                QuestionId = _questionId,
                Text = "answer1",
                OrderBy = 1
            };

            var answerModel = await _createAnswerCommandHandler.Handle(createAnswer, CancellationToken.None);

            Assert.Null(answerModel.Errors);
        }
        public void Dispose() => _context?.Dispose();
    }
}
