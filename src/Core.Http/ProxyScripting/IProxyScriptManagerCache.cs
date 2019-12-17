using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting
{
    public interface IProxyScriptManagerCache
    {
        string GetOrAdd(string key, Func<string> factory);

        void Set(string key, string value);
    }
}
