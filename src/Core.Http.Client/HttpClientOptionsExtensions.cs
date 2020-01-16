using Abp.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client
{
    public static class HttpClientOptionsExtensions
    {
        public static IAbpHttpClientOptions BlogWebCoreModule(this IModuleConfigurations configuration)
        {
            return configuration.AbpConfiguration.Get<IAbpHttpClientOptions>();
        }

    }
}
