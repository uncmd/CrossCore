using Core.Http.Client.DynamicProxying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client
{
    public interface IAbpHttpClientOptions
    {
        Dictionary<Type, DynamicHttpClientProxyConfig> HttpClientProxies { get; set; }
    }
}
