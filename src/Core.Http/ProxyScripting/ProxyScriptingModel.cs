﻿using Abp.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting
{
    public class ProxyScriptingModel
    {
        public string GeneratorType { get; set; }

        public bool UseCache { get; set; }

        public string[] Modules { get; set; }

        public string[] Controllers { get; set; }

        public string[] Actions { get; set; }

        public IDictionary<string, string> Properties { get; set; }

        public ProxyScriptingModel(string generatorType, bool useCache = true)
        {
            GeneratorType = generatorType;
            UseCache = useCache;

            Properties = new Dictionary<string, string>();
        }

        public bool IsPartialRequest()
        {
            return !(Modules.IsNullOrEmpty() && Controllers.IsNullOrEmpty() && Actions.IsNullOrEmpty());
        }
    }
}
