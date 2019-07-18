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

namespace Honoplay.Application.Tests.Answers.Queries.GetAnswersList
{
    public class GetAnswersListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetAnswersListQueryHandler _getAnswersListQueryHandler;
        private readonly int _adminUserId;
        private readonly int _answerId;
        private readonly Guid _tenantId;

        public GetAnswersListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getAnswersListQueryHandler = new GetAnswersListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId)
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

            var answer = new Answer
            {
                CreatedBy = adminUser.Id,
                OrderBy = 2,
                QuestionId = question.Id,
                Text = "testAnswer"
            };
            context.Answers.Add(answer);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getAnswersListQuery = new GetAnswersListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var answerModel = await _getAnswersListQueryHandler.Handle(getAnswersListQuery, CancellationToken.None);

            Assert.Null(answerModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getAnswersListQuery = new GetAnswersListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var answerModel = await _getAnswersListQueryHandler.Handle(getAnswersListQuery, CancellationToken.None);

            Assert.Single(answerModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
