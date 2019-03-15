using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    public class JsConverterCreditCardValidatorTest
    {
        [Fact]
        public void WhenCreditCardNumberIsNotDigit_CreditCardValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                CreditCardValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenCreditCardNumberIsCorrect_CreditCardValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                CreditCardValidatorProp = "5105105105105100"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenCreditCardNumberIsNotCorrect_CreditCardValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                CreditCardValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);
        }
        [Fact]
        public void WhenCreditCardNumberIsNull_CreditCardValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                CreditCardValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validators);
            var expected = TestExtensions.ErrorsCount(seedData, validators);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);
        }
    }
}
