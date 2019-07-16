using FluentValidation.TestHelper;
using Honoplay.Application.Trainers.Queries.GetTrainerDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailValidatorTest : TestBase
    {
        private readonly GetTrainerDetailValidator _validator;

        public GetTrainerDetailValidatorTest()
        {
            _validator = new GetTrainerDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTrainerDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
