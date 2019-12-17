using Core.Http.Client.DynamicProxying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client
{
    public class AbpHttpClientOptions
    {
        public Dictionary<Type, DynamicHttpClientProxyConfig> HttpClientProxies { get; set; }

        public AbpHttpClientOptions()
        {
            HttpClientProxies = new Dictionary<Type, DynamicHttpClientProxyConfig>();
        }
    }
}
