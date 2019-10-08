using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class ContentFileConfiguration : IEntityTypeConfiguration<ContentFile>
    {
        public void Configure(EntityTypeBuilder<ContentFile> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Text
            builder.Property(x => x.Name)
                .HasMaxLength(150);

            //RELATIONS

            //Tenant
            builder.HasOne(c => c.Tenant)
                .WithMany(t => t.ContentFiles)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
