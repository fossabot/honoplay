using System.Globalization;
using FluentValidation.Internal;
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

        public override string GetJs(string propertyName, string errorMessage)
        {
            var replacePropName = propertyName.SplitPascalCase();

            //var errorMessage = LanguageManager.GetString(nameof(MinimumLengthValidator), CultureInfo.CurrentCulture)
            //                                  .Replace(oldValue: "{PropertyName}", newValue: replacePropName)
            //                                  .Replace(oldValue: "{MinLength}", newValue: _validator.Min.ToString())
            //                                  .Replace(oldValue: "{TotalLength}", newValue: $"\" + obj.{propertyName}.length + \"");

            return
                $@"if (obj.{propertyName} !== null && obj.{propertyName}.length < {_validator.Min}){{
                    errors.push(""{errorMessage}"");
                }};";
        }
    }
}
