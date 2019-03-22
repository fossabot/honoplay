﻿using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotNullValidator : AbstractJsConverterValidator<NotNullValidator>
    {
        private readonly NotNullValidator _validator;
        public JsConverterNotNullValidator(NotNullValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorMessage)
        {

            return
                $@"if (obj.{propertyName} == null){{
                    errors.push(""{errorMessage}"");
                }}";
        }

    }
}
