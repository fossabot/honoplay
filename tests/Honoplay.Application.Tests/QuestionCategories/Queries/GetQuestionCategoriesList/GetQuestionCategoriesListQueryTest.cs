using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList;
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

namespace Honoplay.Application.Tests.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public class GetQuestionCategoriesListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionCategoriesListQueryHandler _getQuestionCategoriesListQueryHandler;
        private readonly Guid _tenantId;

        public GetQuestionCategoriesListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getQuestionCategoriesListQueryHandler = new GetQuestionCategoriesListQueryHandler(_context, new CacheManager(cache.Object));
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

            var questionCategory = new QuestionCategory
            {
                Id = 1,
                CreatedBy = adminUser.Id,
                Name = "questionCategory1",

                TenantId = tenant.Id
            };
            context.QuestionCategories.Add(questionCategory);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getQuestionCategoriesListQuery = new GetQuestionCategoriesListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var questionCategoriesResponseModel = await _getQuestionCategoriesListQueryHandler.Handle(getQuestionCategoriesListQuery, CancellationToken.None);

            Assert.Null(questionCategoriesResponseModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getQuestionCategoriesListQuery = new GetQuestionCategoriesListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var questionCategoriesResponseModel = await _getQuestionCategoriesListQueryHandler.Handle(getQuestionCategoriesListQuery, CancellationToken.None);

            Assert.Single(questionCategoriesResponseModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
