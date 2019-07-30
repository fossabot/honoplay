using FluentValidation.TestHelper;
using Honoplay.Application.TrainingSerieses.Queries.GetTrainingSeriesesList;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Queries.GetTrainingSeriesesList
{
    public class GetTrainingSeriesesListValidatorTest
    {
        private readonly GetTrainingSeriesesListValidator _validator;

        public GetTrainingSeriesesListValidatorTest()
        {
            _validator = new GetTrainingSeriesesListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingSeriesesListQueryModel { Take = 5 }).IsValid);
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
