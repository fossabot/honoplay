using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Honoplay.Common.Extensions;

#nullable enable
namespace Honoplay.Common.Tests.Extensions
{
    public sealed class ByteArrayExtensionsTest
    {
        [Fact]
        public void ShouldNullValuesActAsEmptyArrays()
        {
            byte[]? first = null;
            byte[]? second = null;

            var result = first.Combine(second);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void ShouldHaveTwoItemsWhenCombiningTwoOne()
        {
            var first = new byte[] { 1 };
            var second = new byte[] { 2 };
            var result = first.Combine(second);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void ShouldCombinedResultInCorrectOrder()
        {
            var first = new byte[] { 1, 2, 3 };
            var second = new byte[] { 2, 3, 4 };
            var check = new byte[] { 1, 2, 3, 2, 3, 4 };
            var result = first.Combine(second);

            Assert.Equal(check, result);
        }
    }
}
