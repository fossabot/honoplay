using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TraineeUserAvatarConfiguration : IEntityTypeConfiguration<TraineeUserAvatar>
    {
        public void Configure(EntityTypeBuilder<TraineeUserAvatar> builder)
        {
            builder.HasKey(ta => new { ta.AvatarId, ta.TraineeUserId });

            builder
                .HasOne(ta => ta.TraineeUser)
                .WithMany(t => t.TraineeUsersAvatars)
                .HasForeignKey(ta => ta.TraineeUserId);

            builder
                .HasOne(ta => ta.Avatar)
                .WithMany(a => a.TraineeUsersAvatars)
                .HasForeignKey(ta => ta.AvatarId);
        }
    }
}
