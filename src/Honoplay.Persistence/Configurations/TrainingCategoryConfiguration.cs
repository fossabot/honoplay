using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TrainingCategoryConfiguration : IEntityTypeConfiguration<TrainingCategory>
    {
        public void Configure(EntityTypeBuilder<TrainingCategory> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            //Description
            builder.Property(x => x.Description)
                .HasMaxLength(500);


        }
    }
}
