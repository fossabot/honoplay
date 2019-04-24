using Honoplay.Common.Constants;
using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Honoplay.Persistence
{
    public sealed class HonoplayDbContext : DbContext
    {

        private readonly bool _isTest;

        public HonoplayDbContext(DbContextOptions<HonoplayDbContext> options) : base(options)
        {
            _isTest = true;
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantAdminUser> TenantAdminUsers { get; set; }
        public DbSet<TenantDepartment> TenantDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HonoplayDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_isTest)
            {
                return;
            }
            optionsBuilder.UseSqlServer(StringConstants.ConnectionString);
        }
    }
}
