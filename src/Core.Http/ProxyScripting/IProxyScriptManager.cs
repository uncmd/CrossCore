using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting
{
    public interface IProxyScriptManager
    {
        string GetScript(ProxyScriptingModel scriptingModel);
    }
}
