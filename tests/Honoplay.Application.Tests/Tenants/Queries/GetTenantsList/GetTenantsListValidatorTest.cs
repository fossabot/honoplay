using FluentValidation.TestHelper;
using Honoplay.Application.Tenants.Queries.GetTenantsList;
using System;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Queries.GetTenantsList
{
    public class GetTenantsListValidatorTest
    {
        private readonly GetTenantsListValidator _validator;

        public GetTenantsListValidatorTest()
        {
            _validator = new GetTenantsListValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new GetTenantsListQuery()).IsValid);
            Assert.True(_validator.Validate(new GetTenantsListQuery(Guid.NewGuid(), 0, 5)).IsValid);
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Skip, -1);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 4);
            _validator.ShouldHaveValidationErrorFor(x => x.Take, 101);
        }
    }
}