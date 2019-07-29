using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TrainingSeriesConfiguration : IEntityTypeConfiguration<TrainingSeries>
    {
        public void Configure(EntityTypeBuilder<TrainingSeries> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(50);

            //INDEXES
            builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();

            //RELATIONS
            //Tenant
            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.TrainingSerieses)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
