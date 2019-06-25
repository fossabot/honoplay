using FluentValidation;
using FluentValidation.Results;
using FluentValidatorJavascript;
using Jint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidator.Tests.Extensions
{
    public static class TestExtensions
    {
        public static List<string> GetActualErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            var errorsList = new List<string>();

            var abstractValidatorName = abstractValidator.GetType().Name;
            var js = JsConverter.GetJavascript(abstractValidator);

            js = js.Replace(abstractValidatorName + ":function", "function " + abstractValidatorName)
                        .Remove(js.LastIndexOf(",", StringComparison.Ordinal));

            var engine = new Engine().Execute(js);

            dynamic result = (engine.Invoke(abstractValidatorName, seedData)
                .ToObject() as IDictionary<string, object>)?
                .Values
                .FirstOrDefault();

            if (result != null)
            {
                errorsList.Add(result[0].errorKey.ToString());
            }

            return errorsList;

        }
        public static int GetActualErrorCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return GetActualErrors(seedData, abstractValidator).Count;

        }

        private static IList<ValidationFailure> GetExpectErrors<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return abstractValidator.Validate(seedData).Errors;
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
