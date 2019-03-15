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

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);

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

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);

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

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);

        }
    }
}
