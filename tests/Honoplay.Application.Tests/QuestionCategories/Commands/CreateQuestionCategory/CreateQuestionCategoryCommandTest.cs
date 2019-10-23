using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory;
using Xunit;

namespace Honoplay.Application.Tests.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateQuestionCategoryCommandHandler _createQuestionCategoryCommandHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateQuestionCategoryCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId);
            _createQuestionCategoryCommandHandler = new CreateQuestionCategoryCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId)
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
                CreatedBy = adminUser.Id
            });


            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createQuestionCategory = new CreateQuestionCategoryCommand
            {
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                CreateQuestionCategoryModels = new List<CreateQuestionCategoryCommandModel>
                {
                    new CreateQuestionCategoryCommandModel
                    {
                        Name = "questionCategory1"
                    }
                }
            };

            var questionCategoryModel = await _createQuestionCategoryCommandHandler.Handle(createQuestionCategory, CancellationToken.None);

            Assert.Null(questionCategoryModel.Errors);
        }
        public void Dispose() => _context?.Dispose();
    }
}
