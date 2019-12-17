using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Core.Http.Client.DynamicProxying
{
    public interface IDynamicProxyHttpClientFactory
    {
        HttpClient Create();

        HttpClient Create(string name);
    }
}
