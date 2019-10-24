using FluentValidation.TestHelper;
using Honoplay.Application.TrainingCategories.Queries.GetTrainingCategoryDetail;
using Xunit;

namespace Honoplay.Application.Tests.TrainingCategories.Queries.GetTrainingCategoryDetail
{
    public class GetTrainingCategoryDetailValidatorTest
    {
        private readonly GetTrainingCategoryDetailValidator _getTrainingCategoryDetailValidator;

        public GetTrainingCategoryDetailValidatorTest()
        {
            _getTrainingCategoryDetailValidator = new GetTrainingCategoryDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTrainingCategoryDetailValidator.Validate(new GetTrainingCategoryDetailQuery(id: 1)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getTrainingCategoryDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
