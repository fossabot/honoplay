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
            createTraineeGroupCommandModels = new List<CreateTraineeGroupCommandModel> { Name = "yazilim", Name = "tasarim" };

            Assert.True(_validator.Validate(new CreateTraineeGroupCommand { CreateTraineeGroupCommandModels = createTraineeGroupCommandModels }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.TraineeGroups, new List<CreateTraineeGroupCommandModel>());
        }

    }
}
