using System.Linq;
using FluentValidator.Tests.Extensions;
using FluentValidator.Tests.Seed;
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
        public void WhenValueIsNull_NotNullValidatorMessage_ExpectJsConverterValidatorMessageAreSame()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotNullValidatorProp).NotNull();
                NotNullValidatorProp = "asd",
                IBMMakeStuffAndSellIt = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.GetActualErrors(seedData, validationRules).First();
            var expected = TestExtensions.GetExpectErrors(seedData, validationRules).First().ErrorMessage;


            Assert.Equal(expected, actual);
        }

    }
}
