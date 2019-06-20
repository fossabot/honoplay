using System.Collections.Generic;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotEmptyValidator : AbstractJsConverterValidator<NotEmptyValidator>
    {
        public JsConverterNotEmptyValidator(NotEmptyValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return $@"if(!obj.{propertyName} || 0 === obj.{propertyName}.length) {{
                        errors.{propertyName}.push({{'propertyName':'{propertyName}','errorKey':'{errorKey}'}});
                    }}";
        }
    }
}
