using Abp.Modules;
using Core.Http.Client.DynamicProxying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client
{
    [DependsOn(
        typeof(AbpHttpModule)
        )]
    public class AbpHttpClientModule : AbpModule
    {
        public override void PreInitialize()
        {
            DynamicHttpProxyInterceptorRegistrar.Initialize(IocManager);
        }

        public override void Initialize()
        {
            // TODO: 配置AbpRemoteServiceOptions
        }
    }
}
