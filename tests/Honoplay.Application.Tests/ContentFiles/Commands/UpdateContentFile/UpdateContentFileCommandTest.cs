using Honoplay.Application.ContentFiles.Commands.UpdateContentFile;
using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Commands.UpdateContentFile
{
    public class UpdateContentFileCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly UpdateContentFileCommandHandler _updateContentFileCommandHandler;
        private readonly int _adminUserId;
        private readonly Guid _contentFileId;
        private readonly Guid _tenantId;

        public UpdateContentFileCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId, out _contentFileId);
            _updateContentFileCommandHandler = new UpdateContentFileCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId, out Guid contentFileId)
        {
            var context = GetDbContext();

            var salt = ByteArrayExtensions.GetRandomSalt();
            var adminUser = new AdminUser
            {
                Email = "TestAdminUser01@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
            };
            context.AdminUsers.Add(adminUser);

            var tenant = new Tenant
            {
                Name = "TestTenant#01",
                HostName = "localhost"
            };

            context.Tenants.Add(tenant);

            context.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenant.Id,
                AdminUserId = adminUser.Id,
                UpdatedBy = adminUser.Id
            });

            var contentFile = new ContentFile
            {
                Id = Guid.NewGuid(),
                CreatedBy = adminUser.Id,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Name = "contentFile1",
                ContentType = "image/jpeg",
                TenantId = tenant.Id
            };
            context.ContentFiles.Add(contentFile);

            context.SaveChanges();

            adminUserId = adminUser.Id;
            contentFileId = contentFile.Id;
            tenantId = tenant.Id;

            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var updateContentFile = new UpdateContentFileCommand
            {
                Id = _contentFileId,
                TenantId = _tenantId,
                UpdatedBy = _adminUserId,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Name = "contentFile1",
                ContentType = "image/jpeg"
            };

            var contentFileModel = await _updateContentFileCommandHandler.Handle(updateContentFile, CancellationToken.None);

            Assert.Null(contentFileModel.Errors);
        }

        public void Dispose() => _context?.Dispose();
    }
}
