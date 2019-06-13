﻿using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;

namespace Honoplay.System.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(HonoplayDbContext dbContext)
        {
            var tenantId = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a");
            dbContext.Tenants.Add(new Tenant
            {
                Id = tenantId,
                Name = "api-test",
                Description = "test",
                HostName = "localhost",
            });

            var salt = ByteArrayExtensions.GetRandomSalt();

            var adminUser = new AdminUser
            {
                Email = "registered@omegabigdata.com",
                Password = "Passw0rd".GetSHA512(salt),
                PasswordSalt = salt,
                LastPasswordChangeDateTime = DateTime.Today.AddDays(-5),
            };

            dbContext.AdminUsers.Add(adminUser);

            dbContext.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = tenantId,
                AdminUserId = adminUser.Id,
                CreatedBy = adminUser.Id
            });

            var department = new Department
            {
                CreatedBy = adminUser.Id,
                Name = "yazilim",
                TenantId = tenantId
            };
            dbContext.Departments.Add(department);

            var workingStatus = new WorkingStatus
            {
                Name = "Full-Time"
            };
            dbContext.WorkingStatuses.Add(workingStatus);

            var profession = new Profession
            {
                Name = "Web",
                CreatedBy = adminUser.Id
            };
            dbContext.Professions.Add(profession);

            var trainer = new Trainer
            {
                Name = "Emre",
                PhoneNumber = "1234567890",
                Surname = "KAS",
                DepartmentId = department.Id,
                CreatedBy = adminUser.Id,
                Email = "yunuskas55@gmail.com",
                ProfessionId = profession.Id,
            };
            dbContext.Trainers.Add(trainer);

            var trainee = new Trainee
            {
                DepartmentId = department.Id,
                Name = "Yunus Emre",
                CreatedBy = adminUser.Id,
                Gender = 1,
                NationalIdentityNumber = "654654654444",
                PhoneNumber = "053546835411",
                Surname = "KAS",
                WorkingStatusId = workingStatus.Id
            };

            dbContext.Trainees.Add(trainee);

            dbContext.SaveChanges();
        }
    }
}