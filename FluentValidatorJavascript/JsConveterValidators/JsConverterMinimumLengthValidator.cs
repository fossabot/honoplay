using System.Globalization;
using FluentValidation.Resources;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterMinimumLengthValidator : AbstractJsConverterValidator<MinimumLengthValidator>
    {
        private readonly MinimumLengthValidator _validator;
        public JsConverterMinimumLengthValidator(MinimumLengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName)
        {
            LanguageManager languageManager = new LanguageManager();

            return
                $@"if (obj.{propertyName} !== null && obj.{propertyName}.length < {_validator.Min}){{
                    errors.push(""{languageManager.GetString(nameof(MinimumLengthValidator), CultureInfo.CurrentCulture)
                        .Replace("'{PropertyName}'", "'" + propertyName + "'")
                        .Replace("{MinLength}", _validator.Min.ToString())
                        .Replace("{TotalLength}", $"'+obj.{propertyName}.length+'")}"")
                }};";
        }
    }
}
