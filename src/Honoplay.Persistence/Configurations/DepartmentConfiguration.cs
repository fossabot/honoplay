using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();

            //RELATIONS

            //Tenant
            builder.HasOne(x => x.Tenant)
                .WithMany(y => y.Departments)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
