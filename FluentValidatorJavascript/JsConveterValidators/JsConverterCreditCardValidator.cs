using System.Collections;
using System.Collections.Generic;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterCreditCardValidator : AbstractJsConverterValidator<CreditCardValidator>
    {
        public JsConverterCreditCardValidator(CreditCardValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters)
        {
            return
                $@"var value = obj.{propertyName};
                    if (value !== null) {{
                        if (/[0-9-\s]+/.test(value)) {{

                            var nCheck = 0, nDigit = 0, bEven = false;

                            value = value.replace(/\D/g, '');

                            for (var n = value.length - 1; n >= 0; n--) {{
                                var cDigit = value.charAt(n),
                                    nDigit = parseInt(cDigit, 10);

                                if (bEven) {{
                                    if ((nDigit *= 2) > 9) nDigit -= 9;
                                    }}

                                nCheck += nDigit;
                                bEven = !bEven;
                                }}
                            if (!(nCheck % 10) == 0) {{ 
                                        errors.{propertyName}.push({{'propertyName':'{propertyName}','errorKey':'{errorKey}'}});
                                  }}
                        }} else {{
                            errors.{propertyName}.push({{'propertyName':'{propertyName}','errorKey':'{errorKey}'}});
                        }}
                }}";
        }
    }
}
