using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterInclusiveBetweenValidatorTest
    {
        [Fact]
        public void WhenNumberLessThan18_InclusiveBetweenValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = 17
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenNumberGreaterThan60_InclusiveBetweenValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = 61
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenNumberIsNull_InclusiveBetweenValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
    }
}
