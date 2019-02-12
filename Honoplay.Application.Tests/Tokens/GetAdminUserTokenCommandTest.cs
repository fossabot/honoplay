using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Honoplay.Application.Tests.Tokens
{
    public abstract class GetAdminUserTokenCommandTest : TestBase, IDisposable
    {
        private readonly HonoplayDbContext _context;

        public GetAdminUserTokenCommandTest()
        {
            _context = InitAndGetDbContext();
        }
        private HonoplayDbContext InitAndGetDbContext()
        {
            var context = GetDbContext();

            var tenantGuid = Guid.NewGuid();

            context.Tenants.Add(new Tenant
            {
                Id = tenantGuid,
                Name = "TestTenant#01"
            });

            context.AdminUsers.Add(new AdminUser
            {
                TenantId = tenantGuid,
                Username = "TestAdminUser#01",
                Password = new byte[] { },
                PasswordSalt = new byte[] { },
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5)
            });

            context.SaveChanges();

            return context;
        }

        public void Dispose()
        {
            if (_context is null)
            {
                return;
            }
            _context.Dispose();
        }
    }
}
