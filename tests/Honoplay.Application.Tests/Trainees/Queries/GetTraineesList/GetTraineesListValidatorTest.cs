using FluentValidation.TestHelper;
using System;
using Honoplay.Application.Trainees.Queries.GetTraineesList;
using Xunit;

namespace Honoplay.Application.Tests.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListValidatorTest
    {
        private readonly GetTraineesListValidator _validator;

        public GetTraineesListValidatorTest()
        {
            _validator = new GetTraineesListValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(
                new GetTraineesListQueryModel
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
