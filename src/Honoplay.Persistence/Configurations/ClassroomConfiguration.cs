using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            //Code
            builder.Property(x => x.Code)
                .IsRequired();

            builder.HasIndex(x => new { x.TrainingId, x.Name }).IsUnique();

            //RELATIONS
            //Training
            builder.HasOne(x => x.Training)
                .WithMany(x => x.Classrooms)
                .HasForeignKey(x => x.TrainingId);

            //Trainer
            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.Classrooms)
                .HasForeignKey(x => x.TrainerId);
        }
    }
}
