using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public sealed class TenantAdminUserConfiguration : IEntityTypeConfiguration<TenantAdminUser>
    {
        public void Configure(EntityTypeBuilder<TenantAdminUser> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //Indexes
            builder.HasIndex(x => x.Id);

            //Relations
            builder.HasOne(x => x.Tenant)
                    .WithMany(x => x.TenantAdminUsers)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(x => x.TenantId)
                    .HasConstraintName("FK_Tenant_TenantAdminUsers");

            //Relations
            builder.HasOne(x => x.AdminUser)
                    .WithMany(x => x.TenantAdminUsers)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(x => x.AdminUserId)
                    .HasConstraintName("FK_AdminUser_TenantAdminUsers");
        }
    }
}