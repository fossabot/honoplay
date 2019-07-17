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

namespace Honoplay.Application.Tests.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionsListQueryHandler _queryHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public GetQuestionsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _adminUserId);
            _queryHandler = new GetQuestionsListQueryHandler(_context, new CacheManager(cache.Object));
        }

        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int adminUserId)
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

            adminUserId = adminUser.Id;

            var tenant = new Tenant
            {
                Name = "testTenant",
                HostName = "localhost",
                CreatedBy = adminUserId
            };
            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUserId,
                CreatedBy = adminUserId
            });

            context.Questions.Add(new Question
            {
                Duration = 3,
                Text = "testQuestion",
                CreatedBy = adminUserId,
                TenantId = tenant.Id
            });

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var questionsListQuery = new GetQuestionsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var questionModel = await _queryHandler.Handle(questionsListQuery, CancellationToken.None);

            Assert.Null(questionModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var questionsListQuery = new GetQuestionsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var questionModel = await _queryHandler.Handle(questionsListQuery, CancellationToken.None);

            Assert.Single(questionModel.Items);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
