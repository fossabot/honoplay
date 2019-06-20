using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;
using System.Collections.Generic;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterMaximumLengthValidator : AbstractJsConverterValidator<MaximumLengthValidator>
    {
        private readonly MaximumLengthValidator _validator;
        public JsConverterMaximumLengthValidator(MaximumLengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return
                $@"if (obj.{propertyName} !== null && obj.{propertyName}.length > {_validator.Max}) {{
                                        errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }};";
        }
    }
}
