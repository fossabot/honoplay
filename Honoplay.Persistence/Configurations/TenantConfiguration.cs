using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#nullable enable

namespace Honoplay.Persistence.Configurations
{
    public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.HostName)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasAlternateKey(p => p.HostName);

            builder.Property(p => p.Description)
                   .HasMaxLength(250);
        }
    }
}