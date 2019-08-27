using FluentValidation.TestHelper;
using System;
using Honoplay.Application.TraineeUsers.Queries.GetTraineeUsersList;
using Xunit;

namespace Honoplay.Application.Tests.TraineeUsers.Queries.GetTraineeUsersList
{
    public class GetTraineeUsersListValidatorTest
    {
        private readonly GetTraineeUsersListValidator _validator;

        public GetTraineeUsersListValidatorTest()
        {
            _validator = new GetTraineeUsersListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new GetTraineeUsersListQueryModel
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
