using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Description
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            //Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            //INDEXES
            builder.HasIndex(x => new { x.TrainingSeriesId, x.Name }).IsUnique();

            //RELATIONS
            //TrainingSeries
            builder.HasOne(x => x.TrainingSeries)
                .WithMany(x => x.Trainings)
                .HasForeignKey(x => x.TrainingSeriesId);
        }
    }
}
