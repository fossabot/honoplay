using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            //RELATIONS

            //Tenant
            builder.HasOne(q => q.Tenant)
                .WithMany(t => t.Tags)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(q => q.TenantId);

        }
    }
}
