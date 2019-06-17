using FluentValidation;
using FluentValidatorJavascript.IJsConverterValidators;
using System;
using System.Linq;
using System.Text;

namespace FluentValidatorJavascript
{
    public static class JsConverter
    {

        private static readonly ILookup<Type, Type> TypeLookup;

        static JsConverter()
        {
            TypeLookup = typeof(JsConverter).Assembly.GetTypes()
                .Where(t => t.GetInterface(nameof(IJsConverterValidator)) != null && !t.IsAbstract)
                .ToLookup(t => t.BaseType?.GenericTypeArguments[0]);
        }


        public static string GetJavascript<T>(AbstractValidator<T> validator)
        {
            var sb = new StringBuilder();

            sb.AppendLine("function validate(obj) { \r\n");
            sb.AppendLine("var errors = [];");
            var props = typeof(T).GetProperties();

            foreach (var property in props)
            {
                var propertyName = property.Name;
                var validationContext = new ValidationContext(property);

                foreach (var element in validator.CreateDescriptor().GetValidatorsForMember(propertyName))
                {
                    var errorMessage = element.Options.ErrorMessageSource.GetString(validationContext);

                    foreach (var converterType in TypeLookup[element.GetType()])
                    {
                        if (Activator.CreateInstance(converterType, args: element) is IJsConverterValidator converter) sb.AppendLine(converter.GetJs(propertyName, errorMessage));
                    }
                }
            }
            sb.AppendLine("return errors;\r\n}");

            return sb.ToString();
        }
    }
}
