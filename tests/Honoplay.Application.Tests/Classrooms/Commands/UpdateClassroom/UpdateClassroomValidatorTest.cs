using FluentValidation.TestHelper;
using Honoplay.Application.Classrooms.Commands.UpdateClassroom;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Classrooms.Commands.UpdateClassroom
{
    public class UpdateClassroomValidatorTest
    {
        private readonly UpdateClassroomValidator _validator;
        public UpdateClassroomValidatorTest()
        {
            _validator = new UpdateClassroomValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateClassroomCommand
            {
                Id = 1,
                TrainerUserId = 1,
                TrainingId = 1,
                Name = "test",
                BeginDatetime = DateTimeOffset.Now,
                EndDatetime = DateTimeOffset.Now.AddDays(4),
                Location = "adasd"
            }).IsValid);
        }
        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.TrainerUserId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.TrainingId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
