﻿using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class WorkingStatusConfiguration : IEntityTypeConfiguration<WorkingStatus>

    {
        public void Configure(EntityTypeBuilder<WorkingStatus> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
