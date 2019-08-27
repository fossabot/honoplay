using FluentValidation.TestHelper;
using Honoplay.Application.TrainerUsers.Queries.GetTrainerUsersList;
using Xunit;

namespace Honoplay.Application.Tests.TrainerUsers.Queries.GetTrainerUsersList
{
    public class GetTrainerUsersListValidatorTest
    {
        private readonly GetTrainerUsersListValidator _validator;

        public GetTrainerUsersListValidatorTest()
        {
            _validator = new GetTrainerUsersListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new GetTrainerUsersListQueryModel
                {
                    Skip = 1,
                    Take = 5
                }
                ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForGreaterThanLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -5);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 2);
        }

        [Fact]
        public void ShouldBeNotValidForLessThanLength()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 200);
        }
    }
}
