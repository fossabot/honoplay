using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class LevelConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            //RELATIONS
            //Game
            builder.HasOne(x => x.Game)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.GameId);

            //Classroom
            builder.HasOne(x => x.Classroom)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.ClassroomId);
        }
    }
}
