using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.Answers.Commands.UpdateAnswer;
using Xunit;

namespace Honoplay.Application.Tests.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerValidatorTest : AbstractValidator<UpdateAnswerCommand>
    {
        private readonly UpdateAnswerValidator _updateAnswerValidator;

        public UpdateAnswerValidatorTest()
        {
            _updateAnswerValidator = new UpdateAnswerValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateAnswerValidator.Validate(
                new UpdateAnswerCommand
                {
                    QuestionId = 1,
                    Text = "answer1",
                    OrderBy = 1,
                    Id = 1
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateAnswerValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _updateAnswerValidator.ShouldHaveValidationErrorFor(x => x.QuestionId, 0);
            _updateAnswerValidator.ShouldHaveValidationErrorFor(x => x.OrderBy, 0);
        }
    }
}
