using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Honoplay.Application.ContentFiles.Commands.CreateContentFile;
using Xunit;

namespace Honoplay.Application.Tests.ContentFiles.Commands.CreateContentFile
{
    public class CreateContentFileCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;
        private readonly CreateContentFileCommandHandler _createContentFileCommandHandler;
        private readonly int _adminUserId;
        private readonly Guid _tenantId;

        public CreateContentFileCommandTest()
        {
            var cache = new Mock<IDistributedCache>();
            _context = InitAndGetDbContext(out _adminUserId, out _tenantId);
            _createContentFileCommandHandler = new CreateContentFileCommandHandler(_context, new CacheManager(cache.Object));
        }

        //Arrange
        private HonoplayDbContext InitAndGetDbContext(out int adminUserId, out Guid tenantId)
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
                CreatedBy = adminUser.Id
            });


            context.SaveChanges();

            adminUserId = adminUser.Id;
            tenantId = tenant.Id;
            return context;
        }

        [Fact]
        public async Task ShouldGetModelForValidInformation()
        {
            var createContentFile = new CreateContentFileCommand
            {
                TenantId = _tenantId,
                CreatedBy = _adminUserId,
                CreateContentFileModels = new List<CreateContentFileCommandModel>
                {
                    new CreateContentFileCommandModel
                    {
                        Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                        Name = "contentFile1",
                        ContentType = "image/jpeg"
                    }
                }
            };

            var contentFileModel = await _createContentFileCommandHandler.Handle(createContentFile, CancellationToken.None);

            Assert.Null(contentFileModel.Errors);
        }
        public void Dispose() => _context?.Dispose();
    }
}
