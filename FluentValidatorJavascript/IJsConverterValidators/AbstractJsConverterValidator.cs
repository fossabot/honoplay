using System;
using FluentValidation.Resources;
using FluentValidation.Validators;

namespace FluentValidatorJavascript.IJsConverterValidators
{
    public abstract class AbstractJsConverterValidator<T> : IJsConverterValidator where T : IPropertyValidator
    {
        private readonly T _validator;
        private LanguageManager _languageManager;

        protected LanguageManager LanguageManager => _languageManager = _languageManager ?? new LanguageManager();

        protected AbstractJsConverterValidator(T validator)
        {
            _validator = validator;
        }

        public abstract string GetJs(string propertyName);
    }
}
