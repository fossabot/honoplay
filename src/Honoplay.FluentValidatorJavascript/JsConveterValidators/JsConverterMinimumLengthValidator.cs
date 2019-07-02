using System.Collections.Generic;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterMinimumLengthValidator : AbstractJsConverterValidator<MinimumLengthValidator>
    {
        private readonly MinimumLengthValidator _validator;
        public JsConverterMinimumLengthValidator(MinimumLengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return
                $@"if ('{propertyName}' in obj && obj.{propertyName} && obj.{propertyName}.length < {_validator.Min}){{
                    errors.{propertyName} = [];
                    errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }};";
        }
    }
}
