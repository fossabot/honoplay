﻿using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotEmptyValidator : AbstractJsConverterValidator<NotEmptyValidator>
    {
        public JsConverterNotEmptyValidator(NotEmptyValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName)
        {
            return $"\t if(!obj.{propertyName} || 0 === obj.{propertyName}.length) errors.push('NotEmptyValidator'); \n";
        }
    }
}