﻿using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => new { x.TenantId, x.Name }).IsUnique();

            //RELATIONS

            //Tenant
            builder.HasOne(x => x.Tenant)
                .WithMany(y => y.Professions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(x => x.TenantId);
        }
    }
}
