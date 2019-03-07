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
            dbContext.Tenants.Add(new Tenant
            {
                Id = Guid.NewGuid(),
                Name = "api-test",
                Description = "test",
                HostName = "omega",
            });

            var salt = ByteArrayExtensions.GetRandomSalt();
            dbContext.AdminUsers.Add(new AdminUser
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
                CreatedBy = null,
            });

            dbContext.SaveChanges();
        }
    }
}