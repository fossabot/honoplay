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
                EmailValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenEmailValueIsCorrectFormat_EmailValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                EmailValidatorProp = "test@gmail.com"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenEmailValueIsNull_EmailValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                EmailValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
    }
}
