using FluentValidation.TestHelper;
using Honoplay.Application.QuestionCategories.Queries.GetQuestionCategoriesList;
using Xunit;

namespace Honoplay.Application.Tests.QuestionCategories.Queries.GetQuestionCategoriesList
{
    public class GetQuestionCategoriesListValidatorTest
    {
        private readonly GetQuestionCategoriesListValidator _getQuestionCategoriesListValidator;

        public GetQuestionCategoriesListValidatorTest()
        {
            _getQuestionCategoriesListValidator = new GetQuestionCategoriesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getQuestionCategoriesListValidator.Validate(new GetQuestionCategoriesListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getQuestionCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getQuestionCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getQuestionCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
