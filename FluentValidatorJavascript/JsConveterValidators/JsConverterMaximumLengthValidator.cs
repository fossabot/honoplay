﻿using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterMaximumLengthValidator : AbstractJsConverterValidator<MaximumLengthValidator>
    {
        private readonly MaximumLengthValidator _validator;
        public JsConverterMaximumLengthValidator(MaximumLengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName)
        {
            return
                $"\tif (obj.{propertyName} !== null &&obj.{propertyName}.length > {_validator.Max}) errors.push('MaximumLengthValidator');\n";
        }
    }
}