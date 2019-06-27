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
            var validatorTypes = assembly.GetTypes()
                .Where(x =>
                    (x.BaseType?.IsGenericType ?? false)
                    && x.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                .Select(x => x);

            var jsValidatorObjectBuilder = new StringBuilder();
            jsValidatorObjectBuilder.AppendLine("{");

            foreach (var type in validatorTypes)
            {
                try
                {
                    dynamic validator = Activator.CreateInstance(type);
                    jsValidatorObjectBuilder.Append(JsConverter.GetJavascript(validator));
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                    throw;
                }

            }
            jsValidatorObjectBuilder.AppendLine("};");
            return jsValidatorObjectBuilder.ToString();
        }
    }
}
