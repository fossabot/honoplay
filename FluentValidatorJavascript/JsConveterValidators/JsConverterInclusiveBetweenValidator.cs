using FluentValidation.Internal;
using FluentValidation.Resources;
using FluentValidation.Validators;
using FluentValidatorJavascript.IJsConverterValidators;
using System.Globalization;

namespace FluentValidatorJavascript.JsConveterValidators
{
    public class JsConverterInclusiveBetweenValidator : AbstractJsConverterValidator<InclusiveBetweenValidator>
    {
        private readonly InclusiveBetweenValidator _validator;
        public JsConverterInclusiveBetweenValidator(InclusiveBetweenValidator validator) : base(validator)
        {
            _validator = validator;
        }

        public override string GetJs(string propertyName, string errorMessage)
        {

            var replacePropName = propertyName.SplitPascalCase();

            //var errorMessage = LanguageManager.GetString(key: nameof(InclusiveBetweenValidator), culture: CultureInfo.CurrentCulture)
            //                                  .Replace(oldValue: "{PropertyName}", newValue: replacePropName)
            //                                  .Replace(oldValue: "{From}", newValue: _validator.From.ToString())
            //                                  .Replace(oldValue: "{To}", newValue: _validator.To.ToString())
            //                                  .Replace(oldValue: "{Value}", newValue: $"\"+obj.{propertyName}+\"");
            return
                $@"if (obj.{propertyName} !== null && parseInt(obj.{propertyName}) < {_validator.From} || parseInt(obj.{propertyName})> {_validator.To}){{
                        errors.push(""{errorMessage}"");
                }}";
        }
    }
}
