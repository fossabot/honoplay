using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterLengthValidatorTest
    {
        [Fact]
        public void WhenValueLengthGreaterThan6_LengthValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.LengthValidatorProp).Length(1, 6);
                LengthValidatorProp = "loremipsum"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void WhenValueLengthLessThan1_LengthValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.LengthValidatorProp).Length(1, 6);
                LengthValidatorProp = ""
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenValueIsNull_LengthValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.LengthValidatorProp).Length(1, 6);
                LengthValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
    }
}
