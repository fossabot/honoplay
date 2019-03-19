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

            var errorMessage = languageManager.GetString(key: nameof(LengthValidator), culture: CultureInfo.CurrentCulture)
                                              .Replace(oldValue: "{PropertyName}", newValue: replacePropName)
                                              .Replace(oldValue: "{MinLength}", newValue: _validator.Min.ToString())
                                              .Replace(oldValue: "{MaxLength}", newValue: _validator.Max.ToString())
                                              .Replace(oldValue: "{TotalLength}", newValue: $"\" + obj.{propertyName}.length + \"");

            return
                $@"if ((obj.{propertyName} !== null) && (obj.{propertyName}.length < {_validator.Min} || obj.{propertyName}.length > {_validator.Max})) {{
                        errors.push(""{errorMessage}"");
                }}";
        }
    }
}
