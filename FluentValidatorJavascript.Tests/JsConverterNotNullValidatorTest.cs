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

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.Empty(actual);

            Assert.Equal(expected, actual.Count);

        }
        [Fact]
        public void WhenValueIsNull_NotNullValidator_ExpectErrorCount1()
        {
            var seedData = new SeedData
            {
                //RuleFor(rf => rf.NotEmptyValidatorProp).NotEmpty();
                NotNullValidatorProp = null
            };

            var validationRules = new SeedDataValidator();

            var actual = TestExtensions.Invoke(seedData, validationRules);
            var expected = TestExtensions.ErrorsCount(seedData, validationRules);

            Assert.NotEmpty(actual);

            Assert.Equal(expected, actual.Count);

        }

    }
}
