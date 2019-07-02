using System;
using System.Collections.Generic;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterGreaterThanValidator : AbstractJsConverterValidator<GreaterThanValidator>
    {
        private readonly GreaterThanValidator _validator;
        public JsConverterGreaterThanValidator(GreaterThanValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            if (errorKey == null) throw new ArgumentNullException(nameof(errorKey));
            return
                $@"if ('{propertyName}' in obj && obj.{propertyName} && obj.{propertyName}.length < {_validator.ValueToCompare} ) {{
                    errors.{propertyName}= new Array();
                    errors.{propertyName}.push({GetRow(propertyName,errorKey, parameters)});
                }}";
        }
    }
}
