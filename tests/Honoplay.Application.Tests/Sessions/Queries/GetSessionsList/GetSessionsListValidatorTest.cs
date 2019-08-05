using FluentValidation.TestHelper;
using Honoplay.Application.Sessions.Queries.GetSessionsList;
using Xunit;

namespace Honoplay.Application.Tests.Sessions.Queries.GetSessionsList
{
    public class GetSessionsListValidatorTest
    {
        private readonly GetSessionsListValidator _validator;

        public GetSessionsListValidatorTest()
        {
            _validator = new GetSessionsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetSessionsListQueryModel { Take = 5 }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 3);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 102);
        }
    }
}
