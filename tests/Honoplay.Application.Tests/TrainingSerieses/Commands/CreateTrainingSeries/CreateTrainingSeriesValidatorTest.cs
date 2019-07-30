using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Commands.CreateTrainingSeries
{
    public class CreateTrainingSeriesValidatorTest
    {
        private readonly CreateTrainingSeriesValidator _validator;

        public CreateTrainingSeriesValidatorTest()
        {
            _validator = new CreateTrainingSeriesValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateTrainingSeriesCommand
            {
                CreateTrainingSeriesModels = new List<CreateTrainingSeriesCommandModel>
                {
                    new CreateTrainingSeriesCommandModel
                    {
                        Name="testTrainingSeries"
                    }
                }
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
        }
    }
}
