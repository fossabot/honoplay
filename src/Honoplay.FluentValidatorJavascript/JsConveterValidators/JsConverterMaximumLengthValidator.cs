using System.Collections.Generic;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript.JsConveterValidators
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
                $@"if ('{propertyName}' in obj && obj.{propertyName} && obj.{propertyName}.length > {_validator.Max}) {{
                    errors.{propertyName}= new Array();
                    errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }};";
        }
    }
}
