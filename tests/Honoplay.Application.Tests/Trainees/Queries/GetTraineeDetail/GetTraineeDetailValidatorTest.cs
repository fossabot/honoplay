using FluentValidation.TestHelper;
using Honoplay.Application.Trainees.Queries.GetTraineeDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainees.Queries.GetTraineeDetail
{
    public class GetTraineeDetailValidatorTest : TestBase
    {
        private readonly GetTraineeDetailValidator _validator;

        public GetTraineeDetailValidatorTest()
        {
            _validator = new GetTraineeDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTraineeDetailQuery(id: 1, adminUserId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
