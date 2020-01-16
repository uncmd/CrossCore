using Abp.Application.Services;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Http.Client.DynamicProxying
{
    internal static class DynamicHttpProxyInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            //if (typeof(IApplicationService).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            //{
            //    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(DynamicHttpProxyInterceptor<>)));
            //}
        }
    }
}
