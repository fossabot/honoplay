using System;
using Honoplay.Common.Constants;
using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Honoplay.Persistence
{
    public sealed class HonoplayDbContext : DbContext
    {
        public HonoplayDbContext(DbContextOptions<HonoplayDbContext> options) : base(options)
        {
            _isTest = true;
        }
        private bool _isTest = false;
        public HonoplayDbContext()
        {

        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantAdminUser> TenantAdminUsers { get; set; }


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
