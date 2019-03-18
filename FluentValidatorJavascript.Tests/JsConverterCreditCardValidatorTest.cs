using System.Diagnostics.CodeAnalysis;
using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using Xunit;

namespace FluentValidatorJavascript.Tests
{
    [ExcludeFromCodeCoverage]
    public class JsConverterCreditCardValidatorTest
    {
        [Fact]
        public void WhenCreditCardNumberIsNotDigit_CreditCardValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenCreditCardNumberIsCorrect_CreditCardValidator_ExpectErrorCount0()
        {
            
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "5105105105105100"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenCreditCardNumberIsNotCorrect_CreditCardValidator_ExpectErrorCount1()
        {
            
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "asd"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenCreditCardNumberIsNull_CreditCardValidator_ExpectErrorCount0()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = null
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrorCount(seedData, validators);
            var expected = TestExtensions.GetExpectErrorCount(seedData, validators);

            Assert.Equal(expected, actual);
        }   
    }
}
