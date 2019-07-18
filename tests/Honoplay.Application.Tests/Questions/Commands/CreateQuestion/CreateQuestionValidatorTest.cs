using System;
using FluentValidation.TestHelper;
using Honoplay.Application.Questions.Commands.CreateQuestion;
using Xunit;

namespace Honoplay.Application.Tests.Questions.Commands.CreateQuestion
{
    public class CreateQuestionValidatorTest
    {
        private readonly CreateQuestionValidator _createQuestionValidator;

        public CreateQuestionValidatorTest()
        {
            _createQuestionValidator = new CreateQuestionValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_createQuestionValidator.Validate(
                new CreateQuestionCommand
                {
                    Text = "Asagidakilerden hangisi asagidadir?",
                    Duration = 123
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _createQuestionValidator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
            _createQuestionValidator.ShouldHaveValidationErrorFor(x => x.Duration, 0);
        }
    }
}
