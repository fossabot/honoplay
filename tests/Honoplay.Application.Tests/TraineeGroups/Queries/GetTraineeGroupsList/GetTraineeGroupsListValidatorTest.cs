using FluentValidation.TestHelper;
using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Queries.GetTraineeGroupsList
{
    public class GetTraineeGroupsListValidatorTest
    {
        private readonly GetTraineeGroupsListValidator _validator;

        public GetTraineeGroupsListValidatorTest()
        {
            _validator = new GetTraineeGroupsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTraineeGroupsListQueryModel { Take = 5 }).IsValid);
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
