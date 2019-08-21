using Microsoft.EntityFrameworkCore;
using System;

namespace Honoplay.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionMessage(DbUpdateException exception)
        {
            var conflictItemFirstIndex = exception.InnerException.Message.IndexOf(value: ",", comparisonType: StringComparison.Ordinal) + 2;
            var conflictItemLenght = exception.InnerException.Message.IndexOf(value: ")", comparisonType: StringComparison.Ordinal) - conflictItemFirstIndex;

            var conflictItem = exception.InnerException.Message
                .Substring(startIndex: conflictItemFirstIndex,
                    length: conflictItemLenght);

            return conflictItem;
        }
    }
}
