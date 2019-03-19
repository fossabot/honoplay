using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using System.Linq;
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
                NotEmptyValidatorProp = string.Empty
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
        [Fact]
        public void WhenValueIsNull_NotEmptyValidatorMessage_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotEmptyValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsNotEmpty_NotEmptyValidatorMessage_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotEmptyValidatorProp = "sample"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
    }
}
