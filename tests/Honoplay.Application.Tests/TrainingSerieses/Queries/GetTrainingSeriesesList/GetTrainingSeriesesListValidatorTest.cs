using Xunit;
using Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList;

namespace Honoplay.Application.Tests.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public class GetTrainingSeriesesListValidatorTest
    {
        private readonly GetTrainingSeriesListValidator _validator;

        public GetTrainingSeriesListValidatorTest()
        {
            _validator = new GetTrainingSeriesListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingSeriesListQueryModel { Take = 5 }).IsValid);
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
