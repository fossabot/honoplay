using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TraineeConfiguration : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(100);

            //Surname
            builder.Property(x => x.Surname)
                .HasMaxLength(100);

            //PhoneNumber
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            //RELATIONS
            
            //Department
            builder.HasOne(x => x.Department)
                .WithMany(y => y.Trainees)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.DepartmentId);

            //WorkingStatus
            builder.HasOne(x => x.WorkingStatus)
                .WithMany(y => y.Trainees)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.WorkingStatusId);

        }
    }
}
