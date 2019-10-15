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
using Honoplay.Application.Tags.Queries.GetTagDetail;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Queries.GetTagDetail
{
    public class GetTagDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTagDetailQueryHandler _getTagDetailQueryHandler;
        private readonly int _adminUserId;
        private readonly int _tagId;
        private readonly Guid _tenantId;

        public GetTagDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _tagId, out _adminUserId);
            _getTagDetailQueryHandler = new GetTagDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int tagId, out int adminUserId)
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

            var tag = new Tag
            {
                Id = 1,
                CreatedBy = adminUser.Id,
                Name = "testTag",
                ToQuestion = true
            };
            context.Tags.Add(tag);

            var questionTag = new QuestionTag
            {
                TagId = tag.Id,
                QuestionId = question.Id
            };
            context.QuestionTags.Add(questionTag);

            tenantId = tenant.Id;
            tagId = tag.Id;
            adminUserId = adminUser.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var tagDetailQuery = new GetTagDetailQuery(_adminUserId, _tagId, _tenantId);

            var tagDetailResponseModel = await _getTagDetailQueryHandler.Handle(tagDetailQuery, CancellationToken.None);

            Assert.Null(tagDetailResponseModel.Errors);
            Assert.Equal(expected: _context.Tags.First().Name, actual: tagDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var tagDetailQuery = new GetTagDetailQuery(_adminUserId, _tagId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getTagDetailQueryHandler.Handle(tagDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
