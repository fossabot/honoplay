using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.Options.Commands.CreateOption;
using Xunit;

namespace Honoplay.Application.Tests.Options.Commands.CreateOption
{
    public class CreateOptionValidatorTest : AbstractValidator<CreateOptionCommand>
    {
        private readonly CreateOptionValidator _createOptionValidator;

        public CreateOptionValidatorTest()
        {
            _createOptionValidator = new CreateOptionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_createOptionValidator.Validate(
                new CreateOptionCommand
                {
                    QuestionId = 1,
                    Text = "Option1",
                    OrderBy = 2
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _createOptionValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _createOptionValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
            _createOptionValidator.ShouldHaveValidationErrorFor(x => x.OrderBy, 0);
        }
    }
}
