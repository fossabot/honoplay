using Honoplay.Application.ContentFiles.Queries.GetContentFileDetail;
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

namespace Honoplay.Application.Tests.ContentFiles.Queries.GetContentFileDetail
{
    public class GetContentFileDetailQueryTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly GetContentFileDetailQueryHandler _getContentFileDetailQueryHandler;
        private readonly int _adminUserId;
        private readonly Guid _contentFileId;
        private readonly Guid _tenantId;

        public GetContentFileDetailQueryTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _tenantId, out _contentFileId, out _adminUserId);
            _getContentFileDetailQueryHandler = new GetContentFileDetailQueryHandler(_context, new CacheManager(cache.Object));
        }
        private HonoplayDbContext InitAndGetDbContext(out Guid tenantId, out Guid contentFileId, out int adminUserId)
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
            contentFileId = contentFile.Id;
            adminUserId = adminUser.Id;
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var contentFileDetailQuery = new GetContentFileDetailQuery(_adminUserId, _contentFileId, _tenantId);

            var contentFileDetailResponseModel = await _getContentFileDetailQueryHandler.Handle(contentFileDetailQuery, CancellationToken.None);

            Assert.Null(contentFileDetailResponseModel.Errors);
            Assert.Equal(expected: _context.ContentFiles.First().Name, actual: contentFileDetailResponseModel.Items.First().Name, ignoreCase: true);

        }

        [Fact]
        public async Task ShouldThrowErrorWhenInValidInformation()
        {
            var contentFileDetailQuery = new GetContentFileDetailQuery(_adminUserId, Guid.NewGuid(), _tenantId);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await _getContentFileDetailQueryHandler.Handle(contentFileDetailQuery, CancellationToken.None));
        }

        public void Dispose() => _context?.Dispose();
    }
}
