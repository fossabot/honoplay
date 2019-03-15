using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterLengthValidator : AbstractJsConverterValidator<LengthValidator>
    {
        private readonly LengthValidator _validator;
        public JsConverterLengthValidator(LengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName)
        {
            return $@"if ((obj.{propertyName} !== null) && (obj.{propertyName}.length < {_validator.Min} || obj.{propertyName}.length > {_validator.Max}) ) errors.push('LengthValidator');";
        }
    }
}
