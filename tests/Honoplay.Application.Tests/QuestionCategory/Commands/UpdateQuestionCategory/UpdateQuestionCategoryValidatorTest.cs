using FluentValidation;
using FluentValidation.TestHelper;
using Honoplay.Application.QuestionCategories.Commands.UpdateQuestionCategory;
using System;
using Xunit;

namespace Honoplay.Application.Tests.QuestionCategories.Commands.UpdateQuestionCategory
{
    public class UpdateQuestionCategoryValidatorTest : AbstractValidator<UpdateQuestionCategoryCommand>
    {
        private readonly UpdateQuestionCategoryValidator _updateQuestionCategoryValidator;

        public UpdateQuestionCategoryValidatorTest()
        {
            _updateQuestionCategoryValidator = new UpdateQuestionCategoryValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_updateQuestionCategoryValidator.Validate(
                new UpdateQuestionCategoryCommand
                {
                    Id = 1,
                    Name = "questionCategory1"
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _updateQuestionCategoryValidator.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
            _updateQuestionCategoryValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
