using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotNullValidator : AbstractJsConverterValidator<NotNullValidator>
    {

        public JsConverterNotNullValidator(NotNullValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName)
        {
            return $"\tif (obj.{propertyName}===null) errors.push('NotNullValidator');\n";
        }
    }
}
