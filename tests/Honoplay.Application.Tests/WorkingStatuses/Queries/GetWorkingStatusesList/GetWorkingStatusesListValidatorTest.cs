using FluentValidation.TestHelper;
using Honoplay.Application.WorkingStatuses.Queries.GetWorkingStatusesList;
using Xunit;

namespace Honoplay.Application.Tests.WorkingStatuses.Queries.GetWorkingStatusesList
{
    public class GetWorkingStatusesListValidatorTest : TestBase
    {
        private readonly GetWorkingStatusesListValidator _validator;

        public GetWorkingStatusesListValidatorTest()
        {
            _validator = new GetWorkingStatusesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new GetWorkingStatusesListQueryModel
                {
                    Skip = 0,
                    Take = 5
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForGreaterThanLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -5);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, -2);
        }

        [Fact]
        public void ShouldBeNotValidForLessThanLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 200);
        }
    }
}
