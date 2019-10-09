using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
    {
        public void Configure(EntityTypeBuilder<Avatar> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();

            //ImageBytes
            builder.Property(x => x.ImageBytes)
                .IsRequired();

        }
    }
}
