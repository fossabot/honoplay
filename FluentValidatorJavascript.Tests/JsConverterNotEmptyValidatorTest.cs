using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterNotEmptyValidatorTest
    {
        [Fact]
        public void WhenValueIsNotEmpty_NotEmptyValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotEmptyValidatorProp).NotEmpty();
                NotEmptyValidatorProp = "lorem"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenValueIsEmpty_NotEmptyValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotEmptyValidatorProp).NotEmpty();
                NotEmptyValidatorProp = ""
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenValueIsNull_NotEmptyValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotEmptyValidatorProp).NotEmpty();
                NotEmptyValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
    }
}
