using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Honoplay.Persistence.Configurations
{
    public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            //Id
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            //HostName
            builder.Property(p => p.HostName)
                   .IsUnicode(false)
                   .HasMaxLength(150)
                   .IsRequired();
            builder.HasIndex(p => p.HostName).IsUnique();
            
            //Description
            builder.Property(p => p.Description)
                   .HasMaxLength(250);
        }
    }
}