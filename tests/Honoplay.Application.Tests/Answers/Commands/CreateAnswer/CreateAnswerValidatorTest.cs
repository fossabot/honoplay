using FluentValidation;
using Xunit;

namespace Honoplay.Application.Tests.Answers.Commands.CreateAnswer
{
    public class CreateAnswerValidatorTest : AbstractValidator<CreateAnswerCommand>
    {
        private readonly CreateAnswerValidator _createAnswerValidator;

        public CreateAnswerValidatorTest()
        {
            _createAnswerValidator = new CreateAnswerValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_createAnswerValidator.Validate(
                new CreateAnswerCommand
                {
                    QuestionId = 1,
                    Text = "Answer1",
                    OrderBy = 2
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _createAnswerValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _createAnswerValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
            _createAnswerValidator.ShouldHaveValidationErrorFor(x => x.OrderBy, 0);
        }
    }
}
