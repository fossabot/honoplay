using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class ClassroomTraineeConfiguration : IEntityTypeConfiguration<ClassroomTrainee>
    {
        public void Configure(EntityTypeBuilder<ClassroomTrainee> builder)
        {
            builder.HasKey(bc => new { bc.ClassroomId, bc.TraineeId });

            //Tenant
            builder.HasOne(x => x.Classroom)
                .WithMany(x => x.ClassroomTrainees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.ClassroomId)
                .HasConstraintName("FK_Classroom_ClassroomTrainee");

            //AdminUser
            builder.HasOne(x => x.Trainee)
                .WithMany(x => x.ClassroomTrainees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.TraineeId)
                .HasConstraintName("FK_Trainee_ClassroomTrainee");
        }
    }
}
