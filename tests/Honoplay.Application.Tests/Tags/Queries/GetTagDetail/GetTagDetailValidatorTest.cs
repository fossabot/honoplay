using System;
using FluentValidation.TestHelper;
using Honoplay.Application.Tags.Queries.GetTagDetail;
using Xunit;

namespace Honoplay.Application.Tests.Tags.Queries.GetTagDetail
{
    public class GetTagDetailValidatorTest
    {
        private readonly GetTagDetailValidator _getTagDetailValidator;

        public GetTagDetailValidatorTest()
        {
            _getTagDetailValidator = new GetTagDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getTagDetailValidator.Validate(new GetTagDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getTagDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
