using System.Collections.Generic;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotEmptyValidator : AbstractJsConverterValidator<NotEmptyValidator>
    {
        public JsConverterNotEmptyValidator(NotEmptyValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return $@"if(!obj.{propertyName} || 0 === obj.{propertyName}.length) {{
                        errors.{propertyName}= new Array();
                        errors.{propertyName}.push({{'errorKey':'{errorKey}'}});
                    }};";
        }
    }
}
