using FluentValidation.TestHelper;
using Honoplay.Application.Tenants.Commands.UpdateTenant;
using System;
using System.Linq;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantValidatorTest : TestBase
    {
        private readonly UpdateTenantValidator _validator;

        public UpdateTenantValidatorTest()
        {
            _validator = new UpdateTenantValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new UpdateTenantCommand
            {
                Id = Guid.NewGuid(),
                Name = "test name",
                HostName = "localhost",
                Description = "test desc",
                UpdatedBy = 1,
                Logo = new byte[] { 0 },
            }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);

            _validator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);

            _validator.ShouldHaveValidationErrorFor(x => x.HostName, "");
            _validator.ShouldHaveValidationErrorFor(x => x.HostName, null as string);
        }

        [Fact]
        public void ShouldBeNotValidForMinLen()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _validator.ShouldHaveValidationErrorFor(x => x.HostName, "");
        }

        [Fact]
        public void ShouldBeNotValidForMaxLen()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.HostName, string.Join("", Enumerable.Repeat("x", 151)));
            _validator.ShouldHaveValidationErrorFor(x => x.Description, string.Join("", Enumerable.Repeat("x", 251)));
        }
    }
}