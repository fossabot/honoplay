using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.ContentFiles.Queries.GetContentFilesList;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Queries.GetContentFilesList
{
    public class GetContentFilesListQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetContentFilesListQueryHandler _getContentFilesListQueryHandler;
        private readonly Guid _tenantId;

        public GetContentFilesListQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId);
            _getContentFilesListQueryHandler = new GetContentFilesListQueryHandler(_context, new CacheManager(cache.Object));
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

            var contentFile = new ContentFile
            {
                Id = Guid.NewGuid(),
                CreatedBy = adminUser.Id,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Name = "contentFile1",
                ContentType = "image/jpeg"
            };
            context.ContentFiles.Add(contentFile);

            tenantId = tenant.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var getContentFilesListQuery = new GetContentFilesListQuery(tenantId: _tenantId, skip: 0, take: 10);

            var contentFileModel = await _getContentFilesListQueryHandler.Handle(getContentFilesListQuery, CancellationToken.None);

            Assert.Null(contentFileModel.Errors);
        }

        [Fact]
        public async Task ShouldItemsCount1WhenTake1()
        {
            var getContentFilesListQuery = new GetContentFilesListQuery(tenantId: _tenantId, skip: 0, take: 1);

            var contentFileModel = await _getContentFilesListQueryHandler.Handle(getContentFilesListQuery, CancellationToken.None);

            Assert.Single(contentFileModel.Items);
        }

        public void Dispose() => _context?.Dispose();
    }
}
