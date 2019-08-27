﻿using Honoplay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Honoplay.Persistence.Configurations
{
    public class TrainerUserConfiguration : IEntityTypeConfiguration<TrainerUser>
    {
        public void Configure(EntityTypeBuilder<TrainerUser> builder)
        {
            //Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //Mail
            builder.Property(x => x.Email)
                .HasMaxLength(60)
                .IsRequired();
            builder.HasIndex(e => e.Email)
                .HasFilter("[Email] IS NOT NULL")
                .IsUnique();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            //Surname
            builder.Property(x => x.Surname)
                .HasMaxLength(50)
                .IsRequired();

            //PhoneNumber
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15);

            //RELATIONS

            //Profession
            builder.HasOne(x => x.Profession)
                .WithMany(x => x.TrainerUsers)
                .HasForeignKey(x => x.ProfessionId);
            //Department
            builder.HasOne(x => x.Department)
                .WithMany(x => x.TrainerUsers)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
