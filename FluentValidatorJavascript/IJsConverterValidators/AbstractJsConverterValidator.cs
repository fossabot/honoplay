using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Validators;

namespace FluentValidatorJavascript.IJsConverterValidators
{
    public abstract class AbstractJsConverterValidator<T> : IJsConverterValidator where T : IPropertyValidator
    {
        private readonly T _validator;

        protected AbstractJsConverterValidator(T validator)
        {
            _validator = validator;
        }

        public abstract string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters);

        protected static string GetRow(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder();

            sb.Append($"{{'propertyName':'{propertyName}','errorKey':'{errorKey}','parameters':{{");
            sb.Append(string.Join(",", parameters.Select(parameter => $"'{parameter.Key}':'{parameter.Value}'")));
            sb.Append("}}");

            return sb.ToString();
        }
    }
}
