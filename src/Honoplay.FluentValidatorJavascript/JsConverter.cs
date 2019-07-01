﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using Honoplay.FluentValidatorJavascript.IJsConverterValidators;

namespace Honoplay.FluentValidatorJavascript
{
    public static class JsConverter
    {
        private static readonly ILookup<Type, Type> TypeLookup;

        static JsConverter()
        {
            TypeLookup = typeof(JsConverter).Assembly.GetTypes()
                .Where(t => t.GetInterface(nameof(IJsConverterValidator)) != null && !t.IsAbstract)
                .ToLookup(t => t.BaseType?.GenericTypeArguments[0]);
        }

        public static string GetJavascript<T>(AbstractValidator<T> validator)
        {
            var jsBuilder = new StringBuilder();

            jsBuilder.AppendLine(validator.GetType().Name + ":function (obj) { \r\n var errors = {}; \r\n Object.keys(obj).forEach(function(element) {\r\n errors[element] = []; \r\n });");
            var props = typeof(T).GetProperties();

            foreach (var property in props)
            {
                var propertyName = property.Name;
                foreach (var element in validator.CreateDescriptor().GetValidatorsForMember(propertyName))
                {
                    var parameters = new Dictionary<string, object>();
                    var errorKey = element.GetType().Name;
                    switch (element)
                    {
                        case IComparisonValidator cv:
                            parameters.Add(nameof(cv.ValueToCompare), cv.ValueToCompare);
                            break;
                        case IBetweenValidator bv:
                            parameters.Add(nameof(bv.From), bv.From);
                            parameters.Add(nameof(bv.To), bv.To);
                            break;
                        case ILengthValidator lv:
                            parameters.Add(nameof(lv.Min), lv.Min);
                            parameters.Add(nameof(lv.Max), lv.Max);
                            break;
                    }

                    foreach (var converterType in TypeLookup[element.GetType()])
                    {
                        if (Activator.CreateInstance(converterType, args: element) is IJsConverterValidator converter) jsBuilder.AppendLine(converter.GetJs(propertyName, errorKey, parameters));
                    }
                }
            }
            jsBuilder.AppendLine("return errors;\r\n},");
            return jsBuilder.ToString();
        }
    }
}
