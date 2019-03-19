using System.Linq;
using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterCompositeTest
    {
        [Fact]
        public void WhenEmailValueIsNull_NotNullAndEmailAddressValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                CompositeValidatorProp = null
            };
            var validator = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validator);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validator);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsNull_NotNullAndEmailAddressValidator_ExpectJsConverterMessageAreEqual()
        {
            var seedData = new SeedData
            {
                CompositeValidatorProp = null
            };
            var validator = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validator).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validator).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsNotCorrect_NotNullAndEmailAddressValidator_ExpectJsConverterMessageAreEqual()
        {
            var seedData = new SeedData
            {
                CompositeValidatorProp = "sample"
            };
            var validator = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validator).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validator).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenEmailValueIsCorrect_NotNullAndEmailAddressValidator_ExpectJsConverterMessageIsNullAndAreEqual()
        {
            var seedData = new SeedData
            {
                CompositeValidatorProp = "sample@gmail.com"
            };
            var validator = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validator).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validator).FirstOrDefault();

            Assert.Null(actual);
            Assert.Equal(expected, actual);
        }
    }
}
