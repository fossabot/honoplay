using System.Linq;
using Honoplay.FluentValidatorJavascript.Tests.Extensions;
using Honoplay.FluentValidatorJavascript.Tests.Seed;
using Xunit;

namespace Honoplay.FluentValidatorJavascript.Tests
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

        [Fact]
        public void WhenNumberIsNull_InclusiveBetweenValidator_ExpectErrorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = null
            };

            var validationRules = new SeedDataValidator();
            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenNumberIsGreaterThan60_InclusiveBetweenValidator_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = 61
            };
            var validationRules = new SeedDataValidator();
            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenNumberIsLessThan18_InclusiveBetweenValidator_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                // RuleFor(rf => rf.InclusiveBetweenValidatorProp).InclusiveBetweenValidatorProp(18,60);
                InclusiveBetweenValidatorProp = 17
            };
            var validationRules = new SeedDataValidator();
            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

        }
    }
}
