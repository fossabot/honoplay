using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterMinimumLengthValidatorTest
    {
        [Fact]
        public void WhenValueLengthLessThan4_MinimumLengthValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = "lor"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);

        }
        [Fact]
        public void WhenValueLengthGreaterThan4_MinimumLengthValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = "loremipsum"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);

        }
        [Fact]
        public void WhenValueLengthIsNull_MinimumLengthValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);

        }
    }
}
