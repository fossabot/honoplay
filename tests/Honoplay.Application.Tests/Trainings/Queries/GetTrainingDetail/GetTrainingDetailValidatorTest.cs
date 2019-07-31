using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailValidatorTest
    {
        private readonly GetTrainingDetailValidator _validator;

        public GetTrainingDetailValidatorTest()
        {
            _validator = new GetTrainingDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainingDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
