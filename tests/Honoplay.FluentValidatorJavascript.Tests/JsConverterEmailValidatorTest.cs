using System.Linq;
using Honoplay.FluentValidatorJavascript.Tests.Extensions;
using Honoplay.FluentValidatorJavascript.Tests.Seed;
using Xunit;

namespace Honoplay.FluentValidatorJavascript.Tests
{
    public class JsConverterEmailValidatorTest
    {

        [Fact]
        public void WhenEmailValueIsNotCorrectFormat_EmailValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsCorrectFormat_EmailValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "test@gmail.com"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsNull_EmailValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenEmailValueIsNotCorrectFormat_EmailValidator_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "sample"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validators).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validators).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenEmailValueIsCorrectFormat_EmailValidator_ExpectJsConverterValidatorMessageIsNullAndMessageAreEquals()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.EmailValidatorProp).EmailAddress();
                EmailValidatorProp = "sample@gmail.com"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validators).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validators).FirstOrDefault();

            Assert.Null(actual);
            Assert.Equal(expected, actual);
        }
    }
}
