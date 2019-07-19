using Honoplay.Application.Answers.Queries.GetAnswerDetail;
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

namespace Honoplay.Application.Tests.Answers.Queries.GetAnswerDetail
{
    public class GetAnswerDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetAnswerDetailQueryHandler _getAnswerDetailQueryHandler;
        private readonly int _adminUserId;
        private readonly int _answerId;
        private readonly Guid _tenantId;

        public GetAnswerDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _answerId, out _adminUserId);
            _getAnswerDetailQueryHandler = new GetAnswerDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out int answerId, out int adminUserId)
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

            var answer = new Answer
            {
                Id=1,
                CreatedBy = adminUser.Id,
                OrderBy = 2,
                QuestionId = question.Id,
                Text = "testAnswer",
            };
            context.Answers.Add(answer);

            tenantId = tenant.Id;
            answerId = answer.Id;
            adminUserId = adminUser.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var answerDetailQuery = new GetAnswerDetailQuery(_adminUserId, _answerId, _tenantId);

            var answerDetailResponseModel = await _getAnswerDetailQueryHandler.Handle(answerDetailQuery, CancellationToken.None);

            Assert.Null(answerDetailResponseModel.Errors);
            Assert.Equal(expected: _context.Answers.First().Text, actual: answerDetailResponseModel.Items.First().Text, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var query = new GetAnswerDetailQuery(_adminUserId, _answerId + 1, _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getAnswerDetailQueryHandler.Handle(query, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
