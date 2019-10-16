using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.QuestionDifficulties.Queries.GetQuestionDifficultiesList;
using Xunit;

namespace Honoplay.Application.Tests.QuestionDifficulties.Queries.GetQuestionDifficultiesList
{
    public class GetQuestionDifficultiesListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionDifficultiesListQueryHandler _getQuestionDifficultiesListQueryHandler;

        public GetQuestionDifficultiesListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext();
            _getQuestionDifficultiesListQueryHandler = new GetQuestionDifficultiesListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext()
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

            var questionDifficulty = new Domain.Entities.QuestionDifficulty
            {
                Id = 1,
                Name = "questionDifficulty1",
            };
            context.QuestionDifficulties.Add(questionDifficulty);

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getQuestionDifficultiesListQuery = new GetQuestionDifficultiesListQuery(skip: 0, take: 10);

            var questionDifficultyModel = await _getQuestionDifficultiesListQueryHandler.Handle(getQuestionDifficultiesListQuery, CancellationToken.None);

            Assert.Null(questionDifficultyModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getQuestionDifficultiesListQuery = new GetQuestionDifficultiesListQuery(skip: 0, take: 1);

            var questionDifficultyModel = await _getQuestionDifficultiesListQueryHandler.Handle(getQuestionDifficultiesListQuery, CancellationToken.None);

            Assert.Single(questionDifficultyModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
