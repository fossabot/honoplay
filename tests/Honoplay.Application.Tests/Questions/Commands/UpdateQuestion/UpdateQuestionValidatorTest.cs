using FluentValidation.TestHelper;
using Honoplay.Application.Questions.Commands.UpdateQuestion;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionValidatorTest
    {
        private readonly UpdateQuestionValidator _updateQuestionValidator;

        public UpdateQuestionValidatorTest()
        {
            _updateQuestionValidator = new UpdateQuestionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateQuestionValidator.Validate(
                new UpdateQuestionCommand
                {
                    Id = 1,
                    Text = "Asagidakilerden hangisi asagidadir?",
                    Duration = 123
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateQuestionValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _updateQuestionValidator.ShouldHaveValidationErrorFor(x => x.Duration, 0);
            _updateQuestionValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
