using FluentValidation.TestHelper;
using Honoplay.Application.Tenants.Queries.GetTenantDetail;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantDetail
{
    public class GetTenantDetailValidatorTest : TestBase
    {
        private readonly GetTenantDetailValidator _validator;

        public GetTenantDetailValidatorTest()
        {
            _validator = new GetTenantDetailValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTenantDetailQuery(Guid.NewGuid())).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
        }
    }
}