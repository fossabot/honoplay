using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public sealed class TenantAdminUserConfiguration : IEntityTypeConfiguration<TenantAdminUser>
    {
        public void Configure(EntityTypeBuilder<TenantAdminUser> builder)
        {
            //Id
            builder.HasKey(ta => new { ta.TenantId, ta.AdminUserId });

            //RELATIONS

            //Tenant
            builder.HasOne(x => x.Tenant)
                    .WithMany(x => x.TenantAdminUsers)
                    .HasForeignKey(x => x.TenantId)
                    .HasConstraintName("FK_Tenant_TenantAdminUsers");

            //AdminUser
            builder.HasOne(x => x.AdminUser)
                    .WithMany(x => x.TenantAdminUsers)
                    .HasForeignKey(x => x.AdminUserId)
                    .HasConstraintName("FK_AdminUser_TenantAdminUsers");
        }
    }
}