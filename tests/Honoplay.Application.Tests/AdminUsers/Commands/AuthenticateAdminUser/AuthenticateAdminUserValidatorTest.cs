using FluentValidation.TestHelper;
using Honoplay.Application.AdminUsers.Commands.AuthenticateAdminUser;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Honoplay.Application.Tests.Tokens
{
    public class AuthenticateAdminUserValidatorTest : TestBase
    {
        private readonly AuthenticateAdminUserValidator _validator;

        public AuthenticateAdminUserValidatorTest()
        {
            _validator = new AuthenticateAdminUserValidator();
        }

        [Fact]
        public void ShouldBeValid()
        {
            Assert.True(_validator.Validate(new AuthenticateAdminUserCommand { Email = "test@test.com", Password = "123456" }).IsValid);
        }

        [Fact]
        public void ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);

            _validator.ShouldHaveValidationErrorFor(x => x.Password, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }

        [Fact]
        public void ShouldBeNotValidForEmail()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "test[AT]test.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "test@test[DOT]com");
        }

        [Fact]
        public void ShouldBeNotValidForMaxLen()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, $"{string.Join("", Enumerable.Repeat("x", 150))}@1234567890.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Password, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.Password, "12345");
        }
    }
}