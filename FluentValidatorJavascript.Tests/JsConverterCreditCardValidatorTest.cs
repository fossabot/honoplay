using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
using System.Linq;
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
        [Fact]
        public void WhenCreditCardIsNotDigit_CreditCardValidator_ExpectJsConverterValidatorMessageAreEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "sample"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validators).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validators).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenCreditCardNumberIsCorrect_CreditCardValidator_ExpectJsConverterValidatorMessageIsNullAndMessagesAreEqual()
        {

            var seedData = new SeedData
            {
                //RuleFor(rf => rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "5105105105105100"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validators).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validators).FirstOrDefault();

            Assert.Null(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WhenCreditCardNumberIsNotCorrect_CreditCardValidator_ExpectJsConverterValidatorMessagesAreEqual()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf=>rf.CreditCardValidatorProp).CreditCard();
                CreditCardValidatorProp = "sample"
            };

            var validators = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validators).FirstOrDefault();
            var expected = TestExtensions.GetExpectErrorMessages(seedData, validators).FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void WhenCreditCardNumberIsNull_CreditCardValidator_ExpectJsConverterValidatorMessageAreEqual()
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
