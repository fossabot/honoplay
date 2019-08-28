using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class ClassroomTraineeUserConfiguration : IEntityTypeConfiguration<ClassroomTraineeUser>
    {
        public void Configure(EntityTypeBuilder<ClassroomTraineeUser> builder)
        {
            builder.HasKey(bc => new { bc.ClassroomId, bc.TraineeUserId });

            //Tenant
            builder.HasOne(x => x.Classroom)
                .WithMany(x => x.ClassroomTraineeUsers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.ClassroomId)
                .HasConstraintName("FK_Classroom_ClassroomTraineeUser");

            //AdminUser
            builder.HasOne(x => x.TraineeUser)
                .WithMany(x => x.ClassroomTraineeUsers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.TraineeUserId)
                .HasConstraintName("FK_TraineeUser_ClassroomTraineeUser");
        }
    }
}
