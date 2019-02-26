using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honoplay.Persistence.Configurations
{
    public sealed class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            //  Id
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            //  Tenant Id
            builder.Property(e => e.TenantId)
                  .IsRequired();

            builder.HasOne(e => e.Tenant)
                    .WithMany(e => e.AdminUsers)
                    .HasForeignKey(e => e.TenantId)
                    .HasConstraintName("FK_AdminUsers_Tenants");

            //  UserName
            builder.Property(e => e.UserName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(e => new { e.TenantId, e.UserName })
                  .IsUnique();

            //  Name
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            //  Surname
            builder.Property(e => e.Surname)
                   .IsRequired()
                   .HasMaxLength(50);

            //  PhoneNumber
            builder.Property(e => e.PhoneNumber)
                   .IsUnicode(false)
                   .HasMaxLength(50);

            //  Email
            builder.Property(e => e.Email)
                   .IsUnicode(false)
                   .HasMaxLength(50);

            builder.HasIndex(e => new { e.TenantId, e.Email })
             .HasFilter("[Email] IS NOT NULL")
             .IsUnique();

            //  TimeZone
            builder.Property(e => e.TimeZone)
                   .IsRequired()
                   .HasDefaultValue(3);

            //  CreatedBy
            builder.Property(e => e.CreatedBy);

            //  UpdatedBy
            builder.Property(e => e.CreatedDateTime);

            //  UpdatedDateTime

            //  Password

            //  PasswordSalt

            //  LastPasswordChangeDateTime

            //  NumberOfInvalidPasswordAttemps

            //  RowVersion
            builder.Property(e => e.RowVersion)
                .IsRowVersion();
        }
    }
}
