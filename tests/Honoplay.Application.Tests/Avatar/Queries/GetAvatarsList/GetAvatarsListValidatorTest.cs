using FluentValidation.TestHelper;
using Xunit;

namespace Honoplay.Application.Tests.Avatars.Queries.GetAvatarsList
{
    public class GetAvatarsListValidatorTest
    {
        private readonly GetAvatarsListValidator _getAvatarsListValidator;

        public GetAvatarsListValidatorTest()
        {
            _getAvatarsListValidator = new GetAvatarsListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getAvatarsListValidator.Validate(new GetAvatarsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _getAvatarsListValidator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _getAvatarsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _getAvatarsListValidator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
