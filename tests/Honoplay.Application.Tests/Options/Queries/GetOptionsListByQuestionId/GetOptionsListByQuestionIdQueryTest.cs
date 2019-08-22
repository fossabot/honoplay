using Honoplay.Application.Options.Queries.GetOptionsListByQuestionId;
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

namespace Honoplay.Application.Tests.Options.Queries.GetOptionsListByQuestionId
{
    public class GetOptionsListByQuestionIdQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetOptionsListByQuestionIdQueryHandler _getOptionsListByQuestionIdQueryHandler;
        private readonly int _questionId;
        private readonly Guid _tenantId;

        public GetOptionsListByQuestionIdQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _questionId);
            _getOptionsListByQuestionIdQueryHandler = new GetOptionsListByQuestionIdQueryHandler(_context, new CacheManager(cache.Object));
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

            var option = new Option
            {
                Id = 1,
                CreatedBy = adminUser.Id,
                VisibilityOrder = 2,
                QuestionId = question.Id,
                Text = "testOption",
            };
            context.Options.Add(option);

            tenantId = tenant.Id;
            questionId = question.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var optionsListByQuestionIdQuery = new GetOptionsListByQuestionIdQuery(_questionId, _tenantId);

            var optionsListByQuestionIdResponseModel = await _getOptionsListByQuestionIdQueryHandler.Handle(optionsListByQuestionIdQuery, CancellationToken.None);

            Assert.Null(optionsListByQuestionIdResponseModel.Errors);
            Assert.Equal(expected: _context.Options.First().Text, actual: optionsListByQuestionIdResponseModel.Items.First().Text, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var optionsListByQuestionIdQuery = new GetOptionsListByQuestionIdQuery(5 + _questionId, _tenantId);

            var optionsListByQuestionIdResponseModel = await _getOptionsListByQuestionIdQueryHandler.Handle(optionsListByQuestionIdQuery, CancellationToken.None);

            Assert.Empty(optionsListByQuestionIdResponseModel.Items);

        }

        public void Dispose() => _context?.Dispose();
    }
}
