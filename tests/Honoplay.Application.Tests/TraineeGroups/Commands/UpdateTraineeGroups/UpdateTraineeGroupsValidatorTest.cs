using FluentValidation.TestHelper;
using Honoplay.Application.TraineeGroups.Commands.UpdateTraineeGroup;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Commands.UpdateTraineeGroup
{
    public class UpdateTraineeGroupCommandValidatorTest
    {
        private readonly UpdateTraineeGroupValidator _validator;
        public UpdateTraineeGroupCommandValidatorTest() => _validator = new UpdateTraineeGroupValidator();

        [Fact]
        public void ShouldBeValid()
        {
            var updateTraineeGroupCommandModels = new List<UpdateTraineeGroupCommandModel>
            {
                new UpdateTraineeGroupCommandModel
                {
                    Name = "yazilim1"
                },
                new UpdateTraineeGroupCommandModel
                {
                    Name = "tasarim2"
                }
            };

            Assert.True(_validator.Validate(new UpdateTraineeGroupCommand { UpdateTraineeGroupCommandModels = updateTraineeGroupCommandModels }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.UpdateTraineeGroupCommandModels, new List<UpdateTraineeGroupCommandModel>());
        }
    }
}
