using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public sealed class WorkingStatusConfiguration : IEntityTypeConfiguration<WorkingStatus>

    {
        public void Configure(EntityTypeBuilder<WorkingStatus> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            //CreatedBy
            builder.Property(x => x.CreatedBy)
                .IsRequired();

            //CreatedAt
            builder.Property(x => x.CreatedAt)
                .IsRequired();

            //RELATIONS

            //Tenant
            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.WorkingStatuses)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
