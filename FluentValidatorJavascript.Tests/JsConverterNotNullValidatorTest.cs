using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using System.Linq;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterNotNullValidatorTest
    {
        [Fact]
        public void WhenValueIsNotNull_NotNullValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = "lorem"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

        }
        [Fact]
        public void WhenValueIsNull_NotNullValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);


            Assert.Equal(expected, actual);

        }

        [Fact]
        public void WhenValueIsNull_NotNullValidatorMessage_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = null
            };

            var validationRules = new SeedDataValidator();


            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsNotNull_NotNullValidatorMessage_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = "asd"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsEmpty_NotNullValidatorMessage_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = string.Empty
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsEmpty_CustomNotNullValidatorMessage_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                CustomNotNullValidatorProp = string.Empty
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }

    }
}
