using FluentValidation.TestHelper;
using Honoplay.Application.Trainers.Commands.CreateTrainer;
using System.Linq;
using Xunit;

namespace Honoplay.Application.Tests.Trainers.Commands.CreateTrainer
{
    public class CreateTrainerValidatorTest
    {
        private readonly CreateTrainerValidator _validator;

        public CreateTrainerValidatorTest()
        {
            _validator = new CreateTrainerValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateTrainerCommand
            {
                Name = "asdasd",
                DepartmentId = 1,
                CreatedBy = 1,
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
            _validator.ShouldHaveValidationErrorFor(x => x.DepartmentId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.Surname, string.Empty);
            _validator.ShouldHaveValidationErrorFor(x => x.ProfessionId, 0);
        }

        [Fact]
        public void ShouldBeNotValidForMaxLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.Surname, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.Email, string.Join("", Enumerable.Repeat("x", 151)));
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, string.Join("", Enumerable.Repeat("x", 21)));
        }

        [Fact]
        public void ShouldBeNotValidForEmailAddress()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "asd");
        }
    }
}
