using System.Collections.Generic;
using Honoplay.Application.Trainings.Commands.CreateTraining;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Commands.CreateTraining
{
    public class CreateTrainingValidatorTest
    {
        private readonly CreateTrainingValidator _validator;

        public CreateTrainingValidatorTest()
        {
            _validator = new CreateTrainingValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateTrainingCommand
            {
                CreateTrainingModels = new List<CreateTrainingCommandModel>
                {
                    new CreateTrainingCommandModel
                    {
                        Name="testTraining",
                        Description="testTraining",
                        TrainingSeriesId=1
                    }
                }
            }).IsValid);
        }
    }
}
