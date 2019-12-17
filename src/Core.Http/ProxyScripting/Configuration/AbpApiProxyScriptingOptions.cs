using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting.Configuration
{
    public class AbpApiProxyScriptingOptions
    {
        public IDictionary<string, Type> Generators { get; }

        public AbpApiProxyScriptingOptions()
        {
            Generators = new Dictionary<string, Type>();
        }
    }
}
