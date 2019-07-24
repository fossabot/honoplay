using Honoplay.Application.Options.Commands.UpdateOption;
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

namespace Honoplay.Application.Tests.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateOptionCommandHandler _updateOptionCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly int _optionId;
        private readonly Guid _tenantId;

        public UpdateOptionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId, out _optionId);
            _updateOptionCommandHandler = new UpdateOptionCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out int questionId, out Guid tenantId, out int optionId)
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

            var option = new Option
            {
                CreatedBy = adminUser.Id,
                VisibilityOrder = 2,
                QuestionId = question.Id,
                Text = "Option1",
            };
            context.Options.Add(option);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            questionId = question.Id;
            tenantId = tenant.Id;
            optionId = option.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateOption = new UpdateOptionCommand
            {
                Id = _optionId,
                TenantId = _tenantId,
                UpdatedBy = _adminUserId,
                QuestionId = _questionId,
                Text = "option1",
                VisibilityOrder = 1
            };

            var optionModel = await _updateOptionCommandHandler.Handle(updateOption, CancellationToken.None);

            Assert.Null(optionModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
