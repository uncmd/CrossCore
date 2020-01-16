using Abp.Application.Services;
using Abp.Modules;
using Core.Http.Client.DynamicProxying;
using Core.Http.Client.IdentityModel;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Http.Client
{
    [DependsOn(
        typeof(AbpHttpModule)
        )]
    public class AbpHttpClientModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AbpHttpClientModule(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            IocManager.Register(typeof(IAbpHttpClientOptions), typeof(AbpHttpClientOptions));
            IocManager.Register<AbpRemoteServiceOptions>();
            IocManager.Register(typeof(IAbpIdentityClientOptions), typeof(AbpIdentityClientOptions));
            IocManager.Register(typeof(DynamicHttpProxyInterceptor<>));

            DynamicHttpProxyInterceptorRegistrar.Initialize(IocManager);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            var httpClientOptions = IocManager.Resolve<IAbpHttpClientOptions>();

            var serviceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t =>
               t.IsInterface && t.IsPublic && typeof(IApplicationService).IsAssignableFrom(t)
           );

            foreach (var serviceType in serviceTypes)
            {
                AddHttpClientProxy(httpClientOptions, serviceType);
            }
        }

        protected void AddHttpClientProxy(
            [NotNull] IAbpHttpClientOptions httpClientOptions,
            [NotNull] Type type,
            [NotNull] string remoteServiceConfigurationName = RemoteServiceConfigurationDictionary.DefaultName,
            bool asDefaultService = true)
        {
            
        }
    }
}
