using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterInclusiveBetweenValidator : AbstractJsConverterValidator<InclusiveBetweenValidator>
    {
        private readonly InclusiveBetweenValidator _validator;
        public JsConverterInclusiveBetweenValidator(InclusiveBetweenValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName)
        {
            return $@"if (obj.{propertyName} !== null && parseInt(obj.{propertyName}) < {_validator.From} || parseInt(obj.{propertyName})> {_validator.To} ) errors.push('InclusiveBetweenValidator');";
        }
    }
}
