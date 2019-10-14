using FluentValidation.TestHelper;
using Honoplay.Application.Tags.Queries.GetTagsList;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Queries.GetTagsList
{
    public class GetTagsListValidatorTest
    {
        private readonly GetTagsListValidator _getTagsListValidator;

        public GetTagsListValidatorTest()
        {
            _getTagsListValidator = new GetTagsListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTagsListValidator.Validate(new GetTagsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getTagsListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getTagsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getTagsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
