using FluentValidation.TestHelper;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using Honoplay.Application.Trainers.Commands.UpdateTrainer;
using Xunit;

namespace Honoplay.Application.Tests.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerValidatorTest : TestBase
    {
        private readonly UpdateTrainerValidator _validator;
        public UpdateTrainerValidatorTest()
        {
            _validator = new UpdateTrainerValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateTrainerCommand
            {
                Id = 1,
                Name = "asdasd",
                DepartmentId = 1,
                UpdatedBy = 1,
                Surname = "asdasd",
                PhoneNumber = "21412312321",
                Email = "asd@gmail.com",
                ProfessionId = 1
            }
            ).IsValid);
        }
        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.Email, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.ProfessionId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.DepartmentId, 0);
        }

        [Fact]
        public void ShouldBeNotValidForEmailAddress()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "asdasdas");
        }
    }
}
