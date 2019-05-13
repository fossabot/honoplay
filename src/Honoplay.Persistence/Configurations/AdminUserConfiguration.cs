using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public sealed class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            //Id
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            //UserName
            builder.Property(e => e.UserName)
                   //.IsRequired()
                   .HasComputedColumnSql("Email")
                   .HasMaxLength(150);

            //Name
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            //Surname
            builder.Property(e => e.Surname)
                   .IsRequired()
                   .HasMaxLength(50);

            //PhoneNumber
            builder.Property(e => e.PhoneNumber)
                   .IsUnicode(false)
                   .HasMaxLength(50);

            //Email
            builder.Property(e => e.Email)
                   .IsUnicode(false)
                   .HasMaxLength(150);

            builder.HasIndex(e => e.Email)
             .HasFilter("[Email] IS NOT NULL")
             .IsUnique();

            //  TimeZone
            builder.Property(e => e.TimeZone)
                   .IsRequired()
                   .HasDefaultValue(3);

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