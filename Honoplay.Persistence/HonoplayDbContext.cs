using System;
using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Honoplay.Persistence
{
    public sealed class HonoplayDbContext : DbContext
    {
        public HonoplayDbContext(DbContextOptions<HonoplayDbContext> options) : base(options)
        {
        }

        public DbSet<AdminUser> AdminUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HonoplayDbContext).Assembly);
        }
    }
}
