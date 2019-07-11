using System.Collections.Generic;
using System.Linq;

namespace Honoplay.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> SkipOrAll<T>(this IEnumerable<T> src, int? skip)
        {
            return skip.HasValue ? src.Skip((int)skip) : src;
        }
        public static IEnumerable<T> TakeOrAll<T>(this IEnumerable<T> src, int? take)
        {
            return take.HasValue ? src.Take((int)take) : src;
        }
    }
}
