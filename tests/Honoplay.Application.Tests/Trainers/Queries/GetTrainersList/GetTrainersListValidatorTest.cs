using FluentValidation.TestHelper;
using Honoplay.Application.Trainers.Queries.GetTrainersList;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListValidatorTest : TestBase
    {
        private readonly GetTrainersListValidator _validator;

        public GetTrainersListValidatorTest()
        {
            _validator = new GetTrainersListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new GetTrainersListQueryModel
                {
                    HostName = "localhost",
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
