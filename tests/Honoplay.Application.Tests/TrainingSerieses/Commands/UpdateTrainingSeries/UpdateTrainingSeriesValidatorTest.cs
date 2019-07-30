using System.Linq;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Commands.UpdateTrainingSeries
{
    public class UpdateTrainingSeriesValidatorTest
    {
        private readonly UpdateTrainingSeriesValidator _validator;

        public UpdateTrainingSeriesValidatorTest()
        {
            _validator = new UpdateTrainingSeriesValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateTrainingSeriesCommand
            {
                Id = 1,
                Name = "asdasd"
            }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForMaxLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Join("", Enumerable.Repeat("x", 51)));
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
