using System.Globalization;
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
            LanguageManager languageManager = new LanguageManager();
            return
                $@"if(!obj.{propertyName} || 0 === obj.{propertyName}.length) {{
                            errors.push(""{languageManager.GetString(nameof(NotEmptyValidator), CultureInfo.CurrentCulture)
                                .Replace("'{PropertyName}'", "'" + propertyName + "'")}"");
                }}";
        }
    }
}
