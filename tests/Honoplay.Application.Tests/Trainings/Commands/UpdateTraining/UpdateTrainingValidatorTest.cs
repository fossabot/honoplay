using Honoplay.Application.Trainings.Commands.UpdateTraining;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Commands.UpdateTraining
{
    public class UpdateTrainingValidatorTest
    {
        private readonly UpdateTrainingValidator _validator;

        public UpdateTrainingValidatorTest()
        {
            _validator = new UpdateTrainingValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateTrainingCommand
            {
                Id = 1,
                TrainingSeriesId = 1,
                Name = "trainingSample",
                Description = "sampleDescription",
                TrainingCategoryId = 1,
                BeginDateTime = DateTimeOffset.Now,
                EndDateTime = DateTimeOffset.Now.AddDays(5),
            }).IsValid);
        }
    }
}
