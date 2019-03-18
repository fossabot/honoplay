using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;
using System.Globalization;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterLengthValidator : AbstractJsConverterValidator<LengthValidator>
    {
        private readonly LengthValidator _validator;
        public JsConverterLengthValidator(LengthValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName)
        {
            LanguageManager languageManager = new LanguageManager();

            var replacePropName = propertyName.SplitPascalCase();

            var errorMessage = languageManager.GetString(nameof(LengthValidator), CultureInfo.CurrentCulture)
                                              .Replace("{PropertyName}", replacePropName)
                                              .Replace("{MinLength}", _validator.Min.ToString())
                                              .Replace("{MaxLength}", _validator.Max.ToString())
                                              .Replace("{TotalLength}", $"'+obj.{propertyName}.length+'");

            return
                $@"if ((obj.{propertyName} !== null) && (obj.{propertyName}.length < {_validator.Min} || obj.{propertyName}.length > {_validator.Max})) {{
                        errors.push(""{errorMessage}"");
                }}";
        }
    }
}
