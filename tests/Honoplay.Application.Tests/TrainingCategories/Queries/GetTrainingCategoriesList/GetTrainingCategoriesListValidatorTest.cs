using FluentValidation.TestHelper;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoriesList;
using Xunit;

namespace Honoplay.Application.Tests.TrainingCategory.Queries.GetTrainingCategoryList
{
    public class GetTrainingCategoryListValidatorTest
    {
        private readonly GetTrainingCategoriesListValidator _getTrainingCategoriesListValidator;

        public GetTrainingCategoryListValidatorTest()
        {
            _getTrainingCategoriesListValidator = new GetTrainingCategoriesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTrainingCategoriesListValidator.Validate(new GetTrainingCategoriesListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getTrainingCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getTrainingCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getTrainingCategoriesListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}