using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TenantDepartmentConfiguration : IEntityTypeConfiguration<TenantDepartment>
    {
        public void Configure(EntityTypeBuilder<TenantDepartment> builder)
        {
            builder.HasKey(x => new { x.DepartmentId, x.TenantId });

            builder.HasOne(x => x.Tenant)
                .WithMany(y => y.TenantDepartments)
                .HasForeignKey(x => x.TenantId);

            builder.HasOne(x => x.Department)
                .WithMany(y => y.TenantDepartments)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
