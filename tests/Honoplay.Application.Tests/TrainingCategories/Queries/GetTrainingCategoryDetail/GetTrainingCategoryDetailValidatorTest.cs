using FluentValidation.TestHelper;
using Honoplay.Application.TraininigCategories.Queries.GetTraininigCategoryDetail;
using Xunit;

namespace Honoplay.Application.Tests.TraininigCategories.Queries.GetTraininigCategoryDetail
{
    public class GetTraininigCategoryDetailValidatorTest
    {
        private readonly GetTraininigCategoryDetailValidator _getTraininigCategoryDetailValidator;

        public GetTraininigCategoryDetailValidatorTest()
        {
            _getTraininigCategoryDetailValidator = new GetTraininigCategoryDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTraininigCategoryDetailValidator.Validate(new GetTraininigCategoryDetailQuery(id: 1)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getTraininigCategoryDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
