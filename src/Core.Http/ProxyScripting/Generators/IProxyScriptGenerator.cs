using Core.Http.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}
