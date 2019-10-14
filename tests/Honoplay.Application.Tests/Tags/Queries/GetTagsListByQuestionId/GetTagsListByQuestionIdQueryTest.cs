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

namespace Honoplay.Application.Tests.Tags.Queries.GetTagsListByQuestionId
{
    public class GetTagsListByQuestionIdQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTagsListByQuestionIdQueryHandler _getTagsListByQuestionIdQueryHandler;
        private readonly int _questionId;
        private readonly Guid _tenantId;

        public GetTagsListByQuestionIdQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _questionId);
            _getTagsListByQuestionIdQueryHandler = new GetTagsListByQuestionIdQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int questionId)
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
                Id = 1,
                Duration = 3,
                Text = "testQuestion",
                CreatedBy = adminUser.Id,
                TenantId = tenant.Id
            };
            context.Questions.Add(question);

            var tag = new Tag
            {
                Id = 1,
                CreatedBy = adminUser.Id,
                Name = "testTag",
            };
            context.Tags.Add(tag);

            var questionTag = new QuestionTag
            {
                QuestionId = question.Id,
                TagId = tag.Id
            };
            context.QuestionTags.Add(questionTag);

            tenantId = tenant.Id;
            questionId = question.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var tagsListByQuestionIdQuery = new GetTagsListByQuestionIdQuery(_questionId, _tenantId);

            var tagsListByQuestionIdResponseModel = await _getTagsListByQuestionIdQueryHandler.Handle(tagsListByQuestionIdQuery, CancellationToken.None);

            Assert.Null(tagsListByQuestionIdResponseModel.Errors);
            Assert.Equal(expected: _context.Tags.First().Name, actual: tagsListByQuestionIdResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var tagsListByQuestionIdQuery = new GetTagsListByQuestionIdQuery(5 + _questionId, _tenantId);

            var tagsListByQuestionIdResponseModel = await _getTagsListByQuestionIdQueryHandler.Handle(tagsListByQuestionIdQuery, CancellationToken.None);

            Assert.Empty(tagsListByQuestionIdResponseModel.Items);

        }

        public void Dispose() => _context?.Dispose();
    }
}
