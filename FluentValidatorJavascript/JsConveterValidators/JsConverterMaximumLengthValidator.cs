using System.Globalization;
using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
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
            var languageManager = new LanguageManager();

            var replacePropName = propertyName.SplitPascalCase();

            var errorMessage = languageManager.GetString(nameof(MinimumLengthValidator), CultureInfo.CurrentCulture)
                                              .Replace("{PropertyName}", replacePropName)
                                              .Replace("{MinLength}", _validator.Max.ToString())
                                              .Replace("{TotalLength}", $"'+obj.{propertyName}.length+'");


            return
                $@"if (obj.{propertyName} !== null &&obj.{propertyName}.length > {_validator.Max}) {{
                    errors.push(""{errorMessage}"");
                }};";
        }
    }
}
