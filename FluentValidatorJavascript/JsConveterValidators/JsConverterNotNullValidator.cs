using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;
using System.Globalization;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterNotNullValidator : AbstractJsConverterValidator<NotNullValidator>
    {
        public JsConverterNotNullValidator(NotNullValidator validator) : base(validator)
        {
        }

        public override string GetJs(string propertyName)
        {
            var languageManager = new LanguageManager();

            var replacePropName = propertyName.SplitPascalCase();

            var errorMessage = languageManager.GetString(key: nameof(NotNullValidator), CultureInfo.CurrentCulture)
                                   .Replace(oldValue: "{PropertyName}",newValue: replacePropName);

            return
                $@"if (obj.{propertyName} === null){{
                    errors.push(""{errorMessage}"");
                }}";
        }
    }
}
