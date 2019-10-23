using FluentValidation;
using Honoplay.Application.QuestionCategories.Commands.CreateQuestionCategory;
using System.Collections.Generic;
using Xunit;

namespace Honoplay.Application.Tests.QuestionCategories.Commands.CreateQuestionCategory
{
    public class CreateQuestionCategoryValidatorTest : AbstractValidator<CreateQuestionCategoryCommand>
    {
        private readonly CreateQuestionCategoryValidator _createQuestionCategoryValidator;

        public CreateQuestionCategoryValidatorTest()
        {
            _createQuestionCategoryValidator = new CreateQuestionCategoryValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            var newQuestionCategory = new List<CreateQuestionCategoryCommandModel>
            {
                new CreateQuestionCategoryCommandModel
                {
                    Name = "questionCategory1"
                }
            };
            Assert.True(_createQuestionCategoryValidator.Validate(new CreateQuestionCategoryCommand { CreateQuestionCategoryModels = newQuestionCategory }).IsValid);
        }

    }
}