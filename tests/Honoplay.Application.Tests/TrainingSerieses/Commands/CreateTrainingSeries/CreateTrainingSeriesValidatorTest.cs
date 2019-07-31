using FluentValidation.TestHelper;
using Honoplay.Application.TrainingSerieses.Commands.CreateTrainingSeries;
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
    }
}
