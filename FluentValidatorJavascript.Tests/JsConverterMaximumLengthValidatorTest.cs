using System.Linq;
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

            var actual = TestExtensions.GetActualErrorCount(seedData, validationRules);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validationRules);

            Assert.Equal(expected, actual);

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

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
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

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);           

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsNull_MaximumLengthValidator_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();


            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueIsNotEmpty_MaximumLengthValidator_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = "sample"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueLengthLessThan6_MaximumLengthValidator_ExpectJsConverterValidatorMessageAreNullAndEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = "sam"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.Null(actual);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenValueLengthGreaterThan6_MaximumLengthValidator_ExpectJsConverterValidatorMessageEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.MaximumLengthValidatorProp).MaximumLength(6);
                MaximumLengthValidatorProp = "sample_sample"
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validationRules).FirstOrDefault();

            Assert.NotNull(actual);

            Assert.Equal(expected, actual);
        }
    }
}

