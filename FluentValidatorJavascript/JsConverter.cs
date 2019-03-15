using FluentValidation;
using FluentValidatorJavascript.IJsConverterValidators;
using System;
using System.Linq;
using System.Text;

namespace FluentValidatorJavascript
{
    public static class JsConverter
    {

        private static ILookup<Type, Type> _typeLookup;

        static JsConverter()

        {
            _typeLookup = typeof(JsConverter).Assembly.GetTypes()
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
                foreach (var element in validator.CreateDescriptor().GetValidatorsForMember(propertyName))
                {
                    foreach (var converterType in _typeLookup[element.GetType()])
                    {
                        if (Activator.CreateInstance(converterType, args: element) is IJsConverterValidator converter) sb.AppendLine(converter.GetJs(propertyName));
                    }
                }
            }
            sb.AppendLine("return errors;\r\n}");
            //sb.AppendLine("log(validate(person));");

            return sb.ToString();
        }
    }
}
