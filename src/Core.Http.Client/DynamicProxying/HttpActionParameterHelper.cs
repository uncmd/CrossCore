using Core.Http.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client.DynamicProxying
{
    internal static class HttpActionParameterHelper
    {
        public static object FindParameterValue(IReadOnlyDictionary<string, object> methodArguments, ParameterApiDescriptionModel apiParameter)
        {
            var value = methodArguments.GetOrDefault(apiParameter.NameOnMethod);
            if (value == null)
            {
                return null;
            }

            if (apiParameter.Name == apiParameter.NameOnMethod)
            {
                return value;
            }

            return DictionaryExtensions.GetValueByPath(value, value.GetType(), apiParameter.Name);
        }
    }
}
