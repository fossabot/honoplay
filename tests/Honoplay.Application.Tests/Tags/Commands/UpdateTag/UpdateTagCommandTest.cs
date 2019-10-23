using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Tags.Commands.UpdateTag;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateTagCommandHandler _updateTagCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly int _tagId;
        private readonly Guid _tenantId;

        public UpdateTagCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId, out _tagId);
            _updateTagCommandHandler = new UpdateTagCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int questionId, out Guid tenantId, out int tagId)
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

            var tag = new Tag
            {
                CreatedBy = adminUser.Id,
                Name = "Tag1",
                ToQuestion = true,
                TenantId = tenant.Id
            };
            context.Tags.Add(tag);

            var questionTag = new QuestionTag
            {
                QuestionId = question.Id,
                TagId = tag.Id
            };
            context.QuestionTags.Add(questionTag);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionId = question.Id;
            tenantId = tenant.Id;
            tagId = tag.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateTag = new UpdateTagCommand
            {
                Id = _tagId,
                TenantId = _tenantId,
                UpdatedBy = _adminUserId,
                ToQuestion = true,
                Name = "tag1"
            };

            var tagModel = await _updateTagCommandHandler.Handle(updateTag, CancellationToken.None);

            Assert.Null(tagModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
