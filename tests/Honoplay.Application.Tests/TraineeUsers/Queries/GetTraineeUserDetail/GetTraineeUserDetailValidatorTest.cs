using FluentValidation.TestHelper;
using Honoplay.Application.TraineeUsers.Queries.GetTraineeUserDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.TraineeUsers.Queries.GetTraineeUserDetail
{
    public class GetTraineeUserDetailValidatorTest : TestBase
    {
        private readonly GetTraineeUserDetailValidator _validator;

        public GetTraineeUserDetailValidatorTest()
        {
            _validator = new GetTraineeUserDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTraineeUserDetailQuery(id: 1, adminUserId: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
