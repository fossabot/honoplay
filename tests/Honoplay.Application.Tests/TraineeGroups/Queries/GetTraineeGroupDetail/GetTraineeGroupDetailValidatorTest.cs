using FluentValidation.TestHelper;
using System;
using Xunit;

namespace Honoplay.Application.Tests.TraineeGroups.Queries.GetTraineeGroupDetail
{
    public class GetTraineeGroupDetailValidatorTest : TestBase
    {
        private readonly GetTraineeGroupDetailValidator _validator;

        public GetTraineeGroupDetailValidatorTest()
        {
            _validator = new GetTraineeGroupDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTraineeGroupDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x, 0);
        }
    }
}