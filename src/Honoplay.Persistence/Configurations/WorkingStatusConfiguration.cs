using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class WorkingStatusConfiguration : IEntityTypeConfiguration<WorkingStatus>

    {
        public void Configure(EntityTypeBuilder<WorkingStatus> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(20);
        }
    }
}
