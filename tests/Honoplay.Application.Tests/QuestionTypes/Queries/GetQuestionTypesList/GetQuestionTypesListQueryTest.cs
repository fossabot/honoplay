using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.QuestionTypes.Queries.GetQuestionTypesList;
using Xunit;

namespace Honoplay.Application.Tests.QuestionTypes.Queries.GetQuestionTypesList
{
    public class GetQuestionTypesListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetQuestionTypesListQueryHandler _getQuestionTypesListQueryHandler;

        public GetQuestionTypesListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext();
            _getQuestionTypesListQueryHandler = new GetQuestionTypesListQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext()
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

            var questionType = new Domain.Entities.QuestionType
            {
                Id = 1,
                Name = "questionType1",
            };
            context.QuestionTypes.Add(questionType);

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getQuestionTypesListQuery = new GetQuestionTypesListQuery(skip: 0, take: 10);

            var questionTypeModel = await _getQuestionTypesListQueryHandler.Handle(getQuestionTypesListQuery, CancellationToken.None);

            Assert.Null(questionTypeModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getQuestionTypesListQuery = new GetQuestionTypesListQuery(skip: 0, take: 1);

            var questionTypeModel = await _getQuestionTypesListQueryHandler.Handle(getQuestionTypesListQuery, CancellationToken.None);

            Assert.Single(questionTypeModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
