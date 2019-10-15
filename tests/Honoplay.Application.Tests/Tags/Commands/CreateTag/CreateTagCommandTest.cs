using Honoplay.Application.Tags.Commands.CreateTag;
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
using Xunit;

namespace Honoplay.Application.Tests.Tags.Commands.CreateTag
{
    public class CreateTagCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateTagCommandHandler _createTagCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateTagCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId);
            _createTagCommandHandler = new CreateTagCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int questionId, out Guid tenantId)
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

            var question = new Question
            {
                TenantId = tenant.Id,
                Duration = 10,
                Text = "adqwd",
                CreatedBy = adminUser.Id,
            };
            context.Questions.Add(question);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionId = question.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createTag = new CreateTagCommand
            {
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                CreateTagModels = new List<CreateTagCommandModel>
                {
                    new CreateTagCommandModel
                    {
                        Name = "tag1"
                    }
                }
            };

            var tagModel = await _createTagCommandHandler.Handle(createTag, CancellationToken.None);

            Assert.Null(tagModel.Errors);
        }
        public void Dispose() => _context?.Dispose();
    }
}
