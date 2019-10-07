using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TraineeUserConfiguration : IEntityTypeConfiguration<TraineeUser>
    {
        public void Configure(EntityTypeBuilder<TraineeUser> builder)
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
                .WithMany(y => y.TraineeUsers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.DepartmentId);
            //TraineeGroup
            builder.HasOne(x => x.TraineeGroup)
                .WithMany(y => y.TraineeUsers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.TraineeGroupId);

            //WorkingStatus
            builder.HasOne(x => x.WorkingStatus)
                .WithMany(y => y.TraineeUsers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.WorkingStatusId);
            //ContentFile
            builder.HasOne(x => x.ContentFile)
                .WithMany(x => x.TraineeUsers)
                .HasForeignKey(x => x.ProfilPhotoId);

        }
    }
}
