using FluentValidation.TestHelper;
using System;
using Honoplay.Application.Sessions.Queries.GetSessionDetail;
using Xunit;

namespace Honoplay.Application.Tests.Sessions.Queries.GetSessionDetail
{
    public class GetSessionDetailValidatorTest
    {
        private readonly GetSessionDetailValidator _validator;

        public GetSessionDetailValidatorTest()
        {
            _validator = new GetSessionDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetSessionDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
