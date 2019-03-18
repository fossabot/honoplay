using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterEmailValidatorTest
    {

        [Fact]
        public void WhenEmailValueIsNotCorrectFormat_EmailValidator_ExpectErrorCount1()
        {   
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsCorrectFormat_EmailValidator_ExpectErrorCount0()
        {   
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "test@gmail.com"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsNull_EmailValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
    }
}
