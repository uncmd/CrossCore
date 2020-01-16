using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Mvc
{
    public class UrlControllerNameNormalizerContext
    {
        public string RootPath { get; }

        public string ControllerName { get; }

        public UrlControllerNameNormalizerContext(string rootPath, string controllerName)
        {
            RootPath = rootPath;
            ControllerName = controllerName;
        }
    }
}
