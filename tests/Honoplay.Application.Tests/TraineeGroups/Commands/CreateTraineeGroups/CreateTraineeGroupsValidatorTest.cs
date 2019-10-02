using FluentValidation.TestHelper;
using Honoplay.Application.TraineeGroups.Commands.CreateTraineeGroup;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Commands.CreateTraineeGroup
{
    public class CreateTraineeGroupCommandValidatorTest
    {
        private readonly CreateTraineeGroupValidator _validator;
        public CreateTraineeGroupCommandValidatorTest()
        {
            _validator = new CreateTraineeGroupValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            var createTraineeGroupCommandModels = new List<CreateTraineeGroupCommandModel>
            {
                new CreateTraineeGroupCommandModel
                {
                    Name = "yazilim"
                },
                new CreateTraineeGroupCommandModel
                {
                    Name = "tasarim"
                }
            };

            Assert.True(_validator.Validate(new CreateTraineeGroupCommand { CreateTraineeGroupCommandModels = createTraineeGroupCommandModels }).IsValid);
        }

    }
}
