using FluentValidation;
using FluentValidatorJavascript;
using Jint;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidator.Tests.Extensions
{
    public static class TestExtensions
    {
        public static IList<string> Invoke<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            var js = JsConverter.GetJavascript(abstractValidator);

            var engine = new Engine().Execute(js);

            return (engine.Invoke("validate", seedData).ToObject() as object[]).Cast<string>().ToArray();

        }
        public static int ErrorsCount<T>(T seedData, AbstractValidator<T> abstractValidator)
        {
            return abstractValidator.Validate(seedData).Errors.Count;
        }
    }
}
