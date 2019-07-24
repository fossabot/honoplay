using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.Options.Queries.GetOptionsList;
using Xunit;

namespace Honoplay.Application.Tests.Options.Queries.GetOptionsList
{
    public class GetOptionsListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetOptionsListQueryHandler _getOptionsListQueryHandler;
        private readonly Guid _tenantId;

        public GetOptionsListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getOptionsListQueryHandler = new GetOptionsListQueryHandler(_context, new CacheManager(cache.Object));
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

            var option = new Option
            {
                CreatedBy = adminUser.Id,
                VisibilityOrder = 2,
                QuestionId = question.Id,
                Text = "testOption"
            };
            context.Options.Add(option);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getOptionsListQuery = new GetOptionsListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var optionModel = await _getOptionsListQueryHandler.Handle(getOptionsListQuery, CancellationToken.None);

            Assert.Null(optionModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getOptionsListQuery = new GetOptionsListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var optionModel = await _getOptionsListQueryHandler.Handle(getOptionsListQuery, CancellationToken.None);

            Assert.Single(optionModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
