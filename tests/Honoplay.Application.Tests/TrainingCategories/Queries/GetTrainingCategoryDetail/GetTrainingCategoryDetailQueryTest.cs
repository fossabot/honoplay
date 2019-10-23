using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail;
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

namespace Honoplay.Application.Tests.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public class GetTrainingCategoryDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetTrainingCategoryDetailQueryHandler _getTrainingCategoryDetailQueryHandler;
        private readonly int _trainingCategoryId;

        public GetTrainingCategoryDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _trainingCategoryId);
            _getTrainingCategoryDetailQueryHandler = new GetTrainingCategoryDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out int trainingCategoryId)
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

            var trainingCategory = new TrainingCategory
            {
                Id = 1,
                Name = "trainingCategory1"
            };
            context.TrainingCategories.Add(trainingCategory);

            trainingCategoryId = trainingCategory.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var trainingCategoryDetailQuery = new GetTrainingCategoryDetailQuery(_trainingCategoryId);

            var trainingCategoryDetailResponseModel = await _getTrainingCategoryDetailQueryHandler.Handle(trainingCategoryDetailQuery, CancellationToken.None);

            Assert.Null(trainingCategoryDetailResponseModel.Errors);
            Assert.Equal(expected: _context.TrainingCategories.First().Name, actual: trainingCategoryDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            //Unregistered questionId
            var trainingCategoryDetailQuery = new GetTrainingCategoryDetailQuery(78979);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getTrainingCategoryDetailQueryHandler.Handle(trainingCategoryDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
