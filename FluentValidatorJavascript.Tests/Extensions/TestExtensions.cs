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
        public static List<string> GetActualErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            var js = JsConverter.GetJavascript(abstractValidator);

            var engine = new Engine().Execute(js);

            return (engine.Invoke("validate", seedData).ToObject() as object[]).Cast<string>().ToList();

        }
        public static int GetActualErrorCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetActualErrors(seedData, abstractValidator).Count;

        }
        public static List<ValidationFailure> GetExpectErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return abstractValidator.Validate(seedData).Errors.ToList();
        }
        public static int GetExpectErrorCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetExpectErrors(seedData, abstractValidator).Count;
        }

        public static List<string> GetExpectErrorMessages<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetExpectErrors(seedData, abstractValidator).Select(x => x.ErrorMessage).ToList();
        }
    }
}
