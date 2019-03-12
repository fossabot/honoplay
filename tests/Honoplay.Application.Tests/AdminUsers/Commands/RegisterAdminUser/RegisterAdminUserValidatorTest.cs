using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Honoplay.Application.AdminUsers.Commands.RegisterAdminUser;
using Xunit;

namespace Honoplay.Application.Tests.AdminUsers.Commands.RegisterAdminUser
{
    public class RegisterAdminUserValidatorTest : TestBase
    {
        private readonly RegisterAdminUserValidator _validator;

        public RegisterAdminUserValidatorTest()
        {
            _validator = new RegisterAdminUserValidator();
        }

        [Fact]
        public async Task ShouldBeValid()
        {
            var item = new RegisterAdminUserCommand
            {
                Email = "test@test.com",
                Password = "123456",
                Name = "John",
                Surname = "Doe"
            };
            Assert.True(_validator.Validate(item).IsValid);
        }

        [Fact]
        public async Task ShouldBeNotValidForNullOrEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);

            _validator.ShouldHaveValidationErrorFor(x => x.Password, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Password, null as string);

            _validator.ShouldHaveValidationErrorFor(x => x.Name, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Name, null as string);

            _validator.ShouldHaveValidationErrorFor(x => x.Surname, "");
            _validator.ShouldHaveValidationErrorFor(x => x.Surname, null as string);
        }

        [Fact]
        public async Task ShouldBeNotValidForEmail()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "test[AT]test.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "test@test[DOT]com");
        }

        [Fact]
        public async Task ShouldBeNotValidForMaxLen()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, $"{string.Join("", Enumerable.Repeat("x", 150))}@1234567890.com");
            _validator.ShouldHaveValidationErrorFor(x => x.Password, string.Join("", Enumerable.Repeat("x", 51)));
            _validator.ShouldHaveValidationErrorFor(x => x.Password, "12345");
        }
    }
}