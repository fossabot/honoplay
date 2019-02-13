using System;
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
            optionsBuilder.UseSqlServer(@"Data Source=fides.omegabigdata.com,1443;User ID=sa;Password=Hedele321?;Initial Catalog=Honoplay;app=Honoplay;MultipleActiveResultSets=True");
        }
    }
}
