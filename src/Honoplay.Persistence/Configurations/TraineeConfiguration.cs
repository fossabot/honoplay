using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TraineeConfiguration : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Surname)
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            //relations
            builder.HasOne(x => x.Department)
                .WithMany(y => y.Trainees)
                .HasForeignKey(x => x.DepartmentId);

            builder.HasOne(x => x.WorkingStatus)
                .WithMany(y => y.Trainees)
                .HasForeignKey(x => x.WorkingStatusId);

        }
    }
}
