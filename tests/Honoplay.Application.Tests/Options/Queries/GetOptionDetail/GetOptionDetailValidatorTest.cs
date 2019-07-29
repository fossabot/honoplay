using System;
using FluentValidation.TestHelper;
using Honoplay.Application.Options.Queries.GetOptionDetail;
using Xunit;

namespace Honoplay.Application.Tests.Options.Queries.GetOptionDetail
{
    public class GetOptionDetailValidatorTest
    {
        private readonly GetOptionDetailValidator _getOptionDetailValidator;

        public GetOptionDetailValidatorTest()
        {
            _getOptionDetailValidator = new GetOptionDetailValidator();
        }
        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_getOptionDetailValidator.Validate(new GetOptionDetailQuery(adminUserId: 1, id: 1, tenantId: Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _getOptionDetailValidator.ShouldHaveValidationErrorFor(x => x.Id, 0);
        }
    }
}
