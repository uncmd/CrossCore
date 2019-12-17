using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Http.Modeling
{
    [Serializable]
    public class MethodParameterApiDescriptionModel
    {
        public string Name { get; set; }

        public string TypeAsString { get; set; }

        public bool IsOptional { get; set; }

        public object DefaultValue { get; set; }

        private MethodParameterApiDescriptionModel()
        {

        }

        public static MethodParameterApiDescriptionModel Create(ParameterInfo parameterInfo)
        {
            return new MethodParameterApiDescriptionModel
            {
                Name = parameterInfo.Name,
                TypeAsString = parameterInfo.ParameterType.GetFullNameWithAssemblyName(),
                IsOptional = parameterInfo.IsOptional,
                DefaultValue = parameterInfo.HasDefaultValue ? parameterInfo.DefaultValue : null
            };
        }
    }
}
