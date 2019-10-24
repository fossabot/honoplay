using Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory;
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

namespace Honoplay.Application.Tests.QuestionCategories.Commands.UpdateQuestionCategory
{
    public class UpdateQuestionCategoryCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateQuestionCategoryCommandHandler _updateQuestionCategoryCommandHandler;
        private readonly int _adminUserId;
        private readonly int _questionCategoryId;
        private readonly Guid _tenantId;

        public UpdateQuestionCategoryCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId, out _questionCategoryId);
            _updateQuestionCategoryCommandHandler = new UpdateQuestionCategoryCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId, out int questionCategoryId)
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

            var questionCategory = new QuestionCategory
            {
                Id = 1,
                CreatedBy = adminUser.Id,
                Name = "questionCategory1",
                TenantId = tenant.Id
            };
            context.QuestionCategories.Add(questionCategory);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionCategoryId = questionCategory.Id;
            tenantId = tenant.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateQuestionCategory = new UpdateQuestionCategoryCommand
            {
                Id = _questionCategoryId,
                TenantId = _tenantId,
                UpdatedBy = _adminUserId,
                Name = "questionCategory1",
            };

            var questionCategoryModel = await _updateQuestionCategoryCommandHandler.Handle(updateQuestionCategory, CancellationToken.None);

            Assert.Null(questionCategoryModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
