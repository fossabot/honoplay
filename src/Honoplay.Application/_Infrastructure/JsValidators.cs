using FluentValidation;
using FluentValidatorJavascript;
using System;
using System.Linq;
using System.Text;

namespace Honoplay.Application._Infrastructure
{
    public static class JsValidators
    {
        public static string GetAllJsValidations()
        {
            var assembly = AssemblyIdentifier.Get();
            var query = assembly.GetTypes()
                .Where(x =>
                    (x.BaseType?.IsGenericType ?? false)
                    && x.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                .Select(x => x);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("var validations = {");

            foreach (var element in query)
            {
                try
                {
                    dynamic validator = Activator.CreateInstance(element);
                    stringBuilder.Append(JsConverter.GetJavascript(validator));
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                    throw;
                }

            }
            stringBuilder.AppendLine("};");
            return stringBuilder.ToString();
        }
    }
}
