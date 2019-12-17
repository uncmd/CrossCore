using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Modeling
{
    [Serializable]
    public class ControllerInterfaceApiDescriptionModel
    {
        public string TypeAsString { get; set; }

        private ControllerInterfaceApiDescriptionModel()
        {

        }

        public static ControllerInterfaceApiDescriptionModel Create(Type type)
        {
            return new ControllerInterfaceApiDescriptionModel
            {
                TypeAsString = type.GetFullNameWithAssemblyName()
            };
        }
    }
}
