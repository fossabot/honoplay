using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupCommandValidatorTest
    {
        private readonly UpdateTraineeGroupValidator _validator;
        public UpdateTraineeGroupCommandValidatorTest() => _validator = new UpdateTraineeGroupValidator();

        [Fact]
        public void ShouldBeValid() => Assert.True(_validator.Validate(new UpdateTraineeGroupCommand { Name = "aasdqdw" }).IsValid);

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty() => _validator.ShouldHaveValidationErrorFor(x => x.TraineeGroups, new UpdateTraineeGroupCommand());

    }
}
