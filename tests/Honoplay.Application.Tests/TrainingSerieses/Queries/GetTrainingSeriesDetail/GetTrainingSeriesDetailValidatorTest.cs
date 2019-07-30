using System;
using Xunit;

namespace Honoplay.Application.Tests.TrainingSerieses.Queries.GetTrainingSeriesDetail
{
    public class GetTrainingSeriesDetailValidatorTest
    {
        private readonly GetTrainingSeriesDetailValidator _validator;

        public GetTrainingSeriesDetailValidatorTest()
        {
            _validator = new GetTrainingSeriesDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingSeriesDetailQuery(createdBy: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
