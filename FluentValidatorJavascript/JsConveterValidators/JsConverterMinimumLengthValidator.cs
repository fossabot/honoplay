using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterMinimumLengthValidator : AbstractJsConverterValidator<MinimumLengthValidator>
    {
        private readonly MinimumLengthValidator _validator;
        public JsConverterMinimumLengthValidator(MinimumLengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorMessage)
        {
            return
                $@"if (obj.{propertyName} !== null && obj.{propertyName}.length < {_validator.Min}){{
                    errors.{propertyName}.push(""{errorMessage}"");
                }};";
        }
    }
}
