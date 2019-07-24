using Honoplay.Application.Options.Queries.GetOptionDetail;
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

namespace Honoplay.Application.Tests.Options.Queries.GetOptionDetail
{
    public class GetOptionDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetOptionDetailQueryHandler _getOptionDetailQueryHandler;
        private readonly int _adminUserId;
        private readonly int _optionId;
        private readonly Guid _tenantId;

        public GetOptionDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _optionId, out _adminUserId);
            _getOptionDetailQueryHandler = new GetOptionDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int optionId, out int adminUserId)
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
                Id=1,
                CreatedBy = adminUser.Id,
                OrderBy = 2,
                QuestionId = question.Id,
                Text = "testOption",
            };
            context.Options.Add(option);

            tenantId = tenant.Id;
            optionId = option.Id;
            adminUserId = adminUser.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var optionDetailQuery = new GetOptionDetailQuery(_adminUserId, _optionId, _tenantId);

            var optionDetailResponseModel = await _getOptionDetailQueryHandler.Handle(optionDetailQuery, CancellationToken.None);

            Assert.Null(optionDetailResponseModel.Errors);
            Assert.Equal(expected: _context.Options.First().Text, actual: optionDetailResponseModel.Items.First().Text, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetOptionDetailQuery(_adminUserId, _optionId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getOptionDetailQueryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
