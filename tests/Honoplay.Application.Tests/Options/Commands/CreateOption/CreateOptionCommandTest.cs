using Honoplay.Application.Options.Commands.CreateOption;
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

namespace Honoplay.Application.Tests.Options.Commands.CreateOption
{
    public class CreateOptionCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateOptionCommandHandler _createOptionCommandHandler;
        private readonly int _questionId;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateOptionCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _questionId, out _tenantId);
            _createOptionCommandHandler = new CreateOptionCommandHandler(_context, new CacheManager(cache.Object));
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
            var createOption = new CreateOptionCommand
            {
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                CreateOptionModels = new List<CreateOptionCommandModel>
                {
                    new CreateOptionCommandModel
                    {
                        QuestionId = _questionId,
                        Text = "option1",
                        VisibilityOrder = 1
                    }
                }
            };

            var optionModel = await _createOptionCommandHandler.Handle(createOption, CancellationToken.None);

            Assert.Null(optionModel.Errors);
        }
        public void Dispose() => _context?.Dispose();
    }
}
