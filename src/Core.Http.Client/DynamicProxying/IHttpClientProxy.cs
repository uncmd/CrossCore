using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client.DynamicProxying
{
    public interface IHttpClientProxy<out TRemoteService>
    {
        TRemoteService Service { get; }
    }
}
