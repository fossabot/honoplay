﻿using Honoplay.Application.Classrooms.Commands.CreateClassroom;
using System;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomValidatorTest
    {
        private readonly CreateClassroomValidator _validator;

        public CreateClassroomValidatorTest()
        {
            _validator = new CreateClassroomValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateClassroomCommand
            {
                CreatedBy = 1,
                TenantId = Guid.NewGuid(),
                CreateClassroomModels = new List<CreateClassroomCommandModel>
                {
                    new CreateClassroomCommandModel
                    {
                        TrainerUserId = 1,
                        TrainingId = 1,
                        Name = "test",
                        BeginDatetime = DateTimeOffset.Now,
                        Location = "asda",
                        EndDatetime = DateTimeOffset.Now.AddDays(4)
                    }
                }
            }).IsValid);
        }
    }
}
