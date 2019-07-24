using FluentValidation.TestHelper;
using Honoplay.Application.Options.Queries.GetOptionsList;
using Xunit;

namespace Honoplay.Application.Tests.Options.Queries.GetOptionsList
{
    public class GetOptionsListValidatorTest
    {
        private readonly GetOptionsListValidator _getOptionsListValidator;

        public GetOptionsListValidatorTest()
        {
            _getOptionsListValidator = new GetOptionsListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getOptionsListValidator.Validate(new GetOptionsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getOptionsListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getOptionsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getOptionsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
