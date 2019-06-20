using System.Collections.Generic;
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

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return
                $@"if (obj.{propertyName} !== null && parseInt(obj.{propertyName}) < {_validator.From} || parseInt(obj.{propertyName})> {_validator.To}){{
                                            errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }}";
        }
    }
}
