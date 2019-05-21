using FluentValidation.TestHelper;
using Honoplay.Application.Tenants.Queries.GetTrainersList;
using Honoplay.Application.Trainees.Queries.GetTraineeList;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainees.Queries.GetTraineesList
{
    public class GetTraineesListValidatorTest : TestBase
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
                    TenantId = Guid.NewGuid(),
                    Skip = 1,
                    Take = 5
                }
            ).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.TenantId, Guid.Empty);
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
