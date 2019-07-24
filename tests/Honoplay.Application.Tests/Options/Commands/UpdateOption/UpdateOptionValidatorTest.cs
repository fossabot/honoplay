using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.Options.Commands.UpdateOption;
using Xunit;

namespace Honoplay.Application.Tests.Options.Commands.UpdateOption
{
    public class UpdateOptionValidatorTest : AbstractValidator<UpdateOptionCommand>
    {
        private readonly UpdateOptionValidator _updateOptionValidator;

        public UpdateOptionValidatorTest()
        {
            _updateOptionValidator = new UpdateOptionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateOptionValidator.Validate(
                new UpdateOptionCommand
                {
                    QuestionId = 1,
                    Text = "option1",
                    OrderBy = 1,
                    Id = 1
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateOptionValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _updateOptionValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
            _updateOptionValidator.ShouldHaveValidationErrorFor(x => x.OrderBy, 0);
        }
    }
}
