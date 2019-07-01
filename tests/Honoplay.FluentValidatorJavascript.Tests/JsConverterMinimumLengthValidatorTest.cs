using System.Linq;
using Honoplay.FluentValidatorJavascript.Tests.Extensions;
using Honoplay.FluentValidatorJavascript.Tests.Seed;
using Xunit;

namespace Honoplay.FluentValidatorJavascript.Tests
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

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

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

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

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

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenValueIsNull_MinimumLengthValidator_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsNotEmpty_MinimumLengthValidator_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = "sample"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueLengthLessThan4_MinimumLengthValidator_ExpectJsConverterValidatorMessageEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MinimumLengthValidatorProp).MinimumLength(4);
                MinimumLengthValidatorProp = "sam"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.NotNull(actual);

            Assert.Equal(expected, actual);
        }
    }
}
