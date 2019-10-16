using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoryDetail;
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

namespace Honoplay.Application.Tests.QuestionCategories.Queries.GetQuestionCategoryDetail
{
    public class GetQuestionCategoryDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionCategoryDetailQueryHandler _getQuestionCategoryDetailQueryHandler;
        private readonly int _adminUserId;
        private readonly int _questionCategoryId;
        private readonly Guid _tenantId;

        public GetQuestionCategoryDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _questionCategoryId, out _adminUserId);
            _getQuestionCategoryDetailQueryHandler = new GetQuestionCategoryDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int questionCategoryId, out int adminUserId)
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
            questionCategoryId = questionCategory.Id;
            adminUserId = adminUser.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var questionCategoryDetailQuery = new GetQuestionCategoryDetailQuery(_adminUserId, _questionCategoryId, _tenantId);

            var questionCategoryDetailResponseModel = await _getQuestionCategoryDetailQueryHandler.Handle(questionCategoryDetailQuery, CancellationToken.None);

            Assert.Null(questionCategoryDetailResponseModel.Errors);
            Assert.Equal(expected: _context.QuestionCategories.First().Name, actual: questionCategoryDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var questionCategoryDetailQuery = new GetQuestionCategoryDetailQuery(_adminUserId, Guid.NewGuid(), _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getQuestionCategoryDetailQueryHandler.Handle(questionCategoryDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
