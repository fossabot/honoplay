using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterMaximumLengthValidatorTest
    {
        [Fact]
        public void WhenValueLengthGreaterThan6_MaximumLengthValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = "loremipsum"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);

        }
        [Fact]
        public void WhenValueLengthLessThan6_MaximumLengthValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenValueIsNull_MaximumLengthValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
    }
}
