using System.Globalization;
using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
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
            var replacePropName = propertyName.SplitPascalCase();

            var errorMessage = LanguageManager.GetString(key: nameof(NotEmptyValidator), CultureInfo.CurrentCulture)
                                              .Replace(oldValue: "{PropertyName}",newValue: replacePropName);

            return
                $@"if(!obj.{propertyName} || 0 === obj.{propertyName}.length) {{
                            errors.push(""{errorMessage}"");
                }}";
        }
    }
}
