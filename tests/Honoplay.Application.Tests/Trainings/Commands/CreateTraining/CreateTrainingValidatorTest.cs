using Honoplay.Application.Trainings.Commands.CreateTraining;
using System;
using System.Collections.Generic;
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
                        TrainingSeriesId = 1,
                        TrainingCategoryId = 1,
                        Name = "trainingSample",
                        Description = "sampleDescription",
                        EndDateTime = DateTimeOffset.Now.AddDays(5),
                        BeginDateTime = DateTimeOffset.Now
                    }
                }
            }).IsValid);
        }
    }
}
