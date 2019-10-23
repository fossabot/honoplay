using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultyDetail;
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

namespace Honoplay.Application.Tests.QuestionDifficulties.Queries.GetQuestionDifficultyDetail
{
    public class GetQuestionDifficultyDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionDifficultyDetailQueryHandler _getQuestionDifficultyDetailQueryHandler;
        private readonly int _questionDifficultyId;

        public GetQuestionDifficultyDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _questionDifficultyId);
            _getQuestionDifficultyDetailQueryHandler = new GetQuestionDifficultyDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out int questionDifficultyId)
        {
            var context = GetDbContext();
            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Id = 1,
                Email = "test@omegabigdata.com",
                Password = "pass".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTimeOffset.Now.AddDays(-5)
            };
            context.AdminUsers.Add(adminUser);

            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUser.Id
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
                Duration = 3,
                Text = "testQuestion",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            };
            context.Questions.Add(question);

            var questionDifficulty = new QuestionDifficulty
            {
                Id = 1,
                Name = "questionDifficulty1"
            };
            context.QuestionDifficulties.Add(questionDifficulty);

            questionDifficultyId = questionDifficulty.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var questionDifficultyDetailQuery = new GetQuestionDifficultyDetailQuery(_questionDifficultyId);

            var questionDifficultyDetailResponseModel = await _getQuestionDifficultyDetailQueryHandler.Handle(questionDifficultyDetailQuery, CancellationToken.None);

            Assert.Null(questionDifficultyDetailResponseModel.Errors);
            Assert.Equal(expected: _context.QuestionDifficulties.First().Name, actual: questionDifficultyDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            //Unregistered questionId
            var questionDifficultyDetailQuery = new GetQuestionDifficultyDetailQuery(78979);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getQuestionDifficultyDetailQueryHandler.Handle(questionDifficultyDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
