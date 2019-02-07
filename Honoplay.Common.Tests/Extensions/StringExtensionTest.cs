using System;
using Xunit;
using Honoplay.Common.Extensions;

#nullable enable

namespace Honoplay.Common.Tests.Extensions
{
    public class StringExtensionsTest
    {
        [Fact]
        public void ShouldThrowExceptionForNullValue()
        {
            string? value = null;
            Assert.Throws<ArgumentNullException>(() => value.GetSHA512(new byte[] { }));
        }

        [Fact]
        public void ShouldHaveCorrectHash()
        {
            const string value = "Omega";
            byte[] salt = new byte[] { 123, 123, 123 };
            var expected = new byte[] { 213,15,188,206,153,60,130,254,153,23,161,161,34,250,45,174,50,172,195,94,195,228,219,196,69,251,105,138,223,138,3,6,245,214,235,110,29,104,11,225,234,191,62,51,93,122,42,109,154,103,77,8,179,143,7,107,23,216,76,29,181,172,193,113};

            var result = value.GetSHA512(salt);
            Assert.NotNull(result);
            Assert.Equal(expected,result);
        }

        [Fact]
        public void ShouldHaveCorrectHashWithEmptySalt()
        {
            const string value = "Omega";
            var expected = new byte[] { 200,248,249,68,239,140,37,137,198,46,28,76,172,160,213,226,150,32,97,153,95,69,25,154,148,121,166,142,18,78,39,225,233,30,52,195,114,250,76,67,214,165,223,192,91,89,6,229,218,91,94,248,51,50,250,51,253,174,243,101,90,104,194,251};

            var resultEmpty = value.GetSHA512(new byte[0]);
            var resultNull = value.GetSHA512(null);
            Assert.NotNull(resultEmpty);
            Assert.NotNull(resultNull);
            Assert.Equal(resultEmpty, expected);
            Assert.Equal(resultEmpty, resultNull);
        }

    }
}
