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

        public override string GetJs(string propertyName, string errorMessage)
        {
            var replacePropName = propertyName.SplitPascalCase();

            //var errorMessage = LanguageManager.GetString(key: nameof(MaximumLengthValidator), culture: CultureInfo.CurrentCulture)
            //                                  .Replace(oldValue: "{PropertyName}", newValue: replacePropName)
            //                                  .Replace(oldValue: "{MaxLength}", newValue: _validator.Max.ToString())
            //                                  .Replace(oldValue: "{TotalLength}", newValue: $"\" + obj.{propertyName}.length + \"");


            return
                $@"if (obj.{propertyName} !== null &&obj.{propertyName}.length > {_validator.Max}) {{
                    errors.push(""{errorMessage}"");
                }};";
        }
    }
}
