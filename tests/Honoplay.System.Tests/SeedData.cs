using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;

namespace Honoplay.System.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(HonoplayDbContext dbContext)
        {
            var tenantId = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a");
            dbContext.Tenants.Add(new Tenant
            {
                Id = tenantId,
                Name = "api-test",
                Description = "test",
                HostName = "omega",
            });

            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
            };

            dbContext.AdminUsers.Add(adminUser);

            dbContext.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenantId,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            dbContext.SaveChanges();
        }
    }
}