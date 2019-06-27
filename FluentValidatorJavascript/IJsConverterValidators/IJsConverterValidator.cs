using System.Collections.Generic;

namespace FluentValidatorJavascript.IJsConverterValidators
{
    public interface IJsConverterValidator
    {
        string GetJs(string propertyName, string errorKey, IDictionary<string, object> parameters);
    }
}
