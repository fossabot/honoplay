using System.Collections.Generic;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterInclusiveBetweenValidator : AbstractJsConverterValidator<InclusiveBetweenValidator>
    {
        private readonly InclusiveBetweenValidator _validator;
        public JsConverterInclusiveBetweenValidator(InclusiveBetweenValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return
                $@"if ('{propertyName}' in obj && obj.{propertyName}&& parseInt(obj.{propertyName}) < {_validator.From} || parseInt(obj.{propertyName})> {_validator.To}){{
                        errors.{propertyName}= new Array();
                        errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }};";
        }
    }
}
