using FluentValidation;
using FluentValidatorJavascript;
using Jint;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace FluentValidator.Tests.Extensions
{
    public static class TestExtensions
    {
        public static IList<string> GetActualErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            var js = JsConverter.GetJavascript(abstractValidator);

            var engine = new Engine().Execute(js);

            return (engine.Invoke("validate", seedData).ToObject() as object[]).Cast<string>().ToList();

        }
        public static int GetActualErrorCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetActualErrors(seedData, abstractValidator).Count;

        }
        public static IList<ValidationFailure> GetExpectErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return abstractValidator.Validate(seedData).Errors;
        }
        public static int GetExpectErrorCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetExpectErrors(seedData, abstractValidator).Count;
        }
    }
}
