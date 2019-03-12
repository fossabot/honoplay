using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Honoplay.Application.Tenants.Commands.CreateTenant;
using Xunit;

namespace Honoplay.Application.Tests.Tenants.Commands.CreateTenant
{
    public class CreateTenantValidatorTest : TestBase
    {
        private readonly CreateTenantValidator _validator;

        public CreateTenantValidatorTest()
        {
            _validator = new CreateTenantValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new CreateTenantCommand
            {
                Name = "test name",
                HostName = "test host name",
                Description = "test desc",
                CreatedBy = 1,
                Logo = new byte[] { 0 },
            }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
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