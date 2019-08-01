using FluentValidation.TestHelper;
using Honoplay.Application.Trainings.Queries.GetTrainingsList;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListValidatorTest
    {
        private readonly GetTrainingsListValidator _validator;

        public GetTrainingsListValidatorTest()
        {
            _validator = new GetTrainingsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingsListQueryModel { Take = 5 }).IsValid);
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
