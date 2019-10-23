using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList;
using Xunit;

namespace Honoplay.Application.Tests.TrainingCategories.Queries.GetTrainingCategoriesList
{
    public class GetTrainingCategoriesListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTrainingCategoriesListQueryHandler _getTrainingCategoriesListQueryHandler;

        public GetTrainingCategoriesListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext();
            _getTrainingCategoriesListQueryHandler = new GetTrainingCategoriesListQueryHandler(_context, new CacheManager(cache.Object));
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

            var trainingCategory = new Domain.Entities.TrainingCategory
            {
                Id = 1,
                Name = "trainingCategory1"  
            };
            context.TrainingCategories.Add(trainingCategory);

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getTrainingCategoriesListQuery = new GetTrainingCategoriesListQuery(skip: 0, take: 10);

            var trainingCategoryModel = await _getTrainingCategoriesListQueryHandler.Handle(getTrainingCategoriesListQuery, CancellationToken.None);

            Assert.Null(trainingCategoryModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getTrainingCategoriesListQuery = new GetTrainingCategoriesListQuery(skip: 0, take: 1);

            var trainingCategoryModel = await _getTrainingCategoriesListQueryHandler.Handle(getTrainingCategoriesListQuery, CancellationToken.None);

            Assert.Single(trainingCategoryModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
