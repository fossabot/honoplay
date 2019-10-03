using Honoplay.Common.Extensions;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using System;

namespace Honoplay.AdminWebAPI.System.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(HonoplayDbContext dbContext)
        {
            var tenantId = Guid.Parse("b0dfcb00-6195-46a7-834e-c58276c3242a");
            var secondTenantId = Guid.Parse("f3878709-3cba-4ed3-4c03-08d70375909d");
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

            dbContext.Tenants.Add(new Tenant
            {
                Id = secondTenantId,
                Name = "api-tesasdt",
                Description = "test",
                HostName = "localhosttt",
            });

            dbContext.TenantAdminUsers.Add(new TenantAdminUser
            {
                TenantId = secondTenantId,
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
                Name = "Full-Time",
                CreatedBy = adminUser.Id,
                TenantId = tenantId
            };
            dbContext.WorkingStatuses.Add(workingStatus);

            var profession = new Profession
            {
                Name = "Web",
                CreatedBy = adminUser.Id,
                TenantId = tenantId
            };
            dbContext.Professions.Add(profession);

            var trainingSeries = new TrainingSeries
            {
                Name = "series1",
                CreatedBy = adminUser.Id,
                TenantId = tenantId
            };
            dbContext.TrainingSerieses.Add(trainingSeries);


            var trainingCategory = new TrainingCategory
            {
                CreatedBy = adminUser.Id,
                Description = "sample",
                Name = "test"
            };
            dbContext.TrainingCategories.Add(trainingCategory);

            var training = new Training
            {
                TrainingCategoryId = trainingCategory.Id,
                BeginDateTime = DateTimeOffset.Now,
                EndDateTime = DateTimeOffset.Now.AddDays(5),
                CreatedBy = adminUser.Id,
                Description = "description",
                Name = "test",
                TrainingSeriesId = trainingSeries.Id
            };
            dbContext.Trainings.Add(training);

            var trainerUser = new TrainerUser
            {
                Name = "Emre",
                PhoneNumber = "1234567890",
                Surname = "KAS",
                DepartmentId = department.Id,
                CreatedBy = adminUser.Id,
                Email = "yunuskas55@gmail.com",
                ProfessionId = profession.Id
            };
            dbContext.TrainerUsers.Add(trainerUser);

            var traineeUser = new TraineeUser
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

            dbContext.TraineeUsers.Add(traineeUser);

            var question = new Question
            {
                TenantId = tenantId,
                CreatedBy = adminUser.Id,
                Duration = 123,
                Text = "Yukaridaki yukarida midir?",
            };
            dbContext.Questions.Add(question);

            var option = new Option
            {
                VisibilityOrder = 1,
                CreatedBy = adminUser.Id,
                Text = "yukaridadir",
                QuestionId = question.Id
            };
            dbContext.Options.Add(option);

            var classroom = new Classroom
            {
                CreatedBy = adminUser.Id,
                Name = "test",
                TrainerUserId = trainerUser.Id,
                TrainingId = training.Id,
            };
            dbContext.Classrooms.Add(classroom);

            var game = new Game
            {
                Name = "game1"
            };
            dbContext.Games.Add(game);

            var session = new Session
            {
                UpdatedBy = adminUser.Id,
                Name = "test",
                ClassroomId = classroom.Id,
                GameId = game.Id,
            };
            dbContext.Sessions.Add(session);

            dbContext.TraineeGroups.Add(new TraineeGroup
            {
                CreatedBy = adminUser.Id,
                Name = "traineeGroup",
                TenantId = tenantId
            });

            dbContext.SaveChanges();
        }
    }
}