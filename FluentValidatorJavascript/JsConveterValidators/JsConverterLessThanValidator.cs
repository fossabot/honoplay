using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;
using System;
using System.Collections.Generic;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterLessThanValidator : AbstractJsConverterValidator<LessThanValidator>
    {
        private readonly LessThanValidator _validator;
        public JsConverterLessThanValidator(LessThanValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            if (errorKey == null) throw new ArgumentNullException(nameof(errorKey));
            return
                $@"if (obj.{propertyName} !== null && obj.{propertyName}.length > {_validator.ValueToCompare}) {{
                    errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }}";
        }
    }
}
