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
                Description = "sampleDescription"
            }).IsValid);
        }
    }
}
