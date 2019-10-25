using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypeDetail;
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

namespace Honoplay.Application.Tests.QuestionTypes.Queries.GetQuestionTypeDetail
{
    public class GetQuestionTypeDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionTypeDetailQueryHandler _getQuestionTypeDetailQueryHandler;
        private readonly int _questionTypeId;

        public GetQuestionTypeDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _questionTypeId);
            _getQuestionTypeDetailQueryHandler = new GetQuestionTypeDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out int questionTypeId)
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

            var questionType = new QuestionType
            {
                Id = 1,
                Name = "questionType1"
            };
            context.QuestionTypes.Add(questionType);

            questionTypeId = questionType.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var questionTypeDetailQuery = new GetQuestionTypeDetailQuery(_questionTypeId);

            var questionTypeDetailResponseModel = await _getQuestionTypeDetailQueryHandler.Handle(questionTypeDetailQuery, CancellationToken.None);

            Assert.Null(questionTypeDetailResponseModel.Errors);
            Assert.Equal(expected: _context.QuestionTypes.First().Name, actual: questionTypeDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            //Unregistered questionId
            var questionTypeDetailQuery = new GetQuestionTypeDetailQuery(78979);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getQuestionTypeDetailQueryHandler.Handle(questionTypeDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
