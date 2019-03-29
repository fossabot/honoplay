using FluentValidation.Validators;

namespace FluentValidatorJavascript.IJsConverterValidators
{
    public abstract class AbstractJsConverterValidator<T> : IJsConverterValidator where T : IPropertyValidator
    {
        private readonly T _validator;

        protected AbstractJsConverterValidator(T validator)
        {
            _validator = validator;
        }

        public abstract string GetJs(string propertyName, string errorMessage);
    }
}
