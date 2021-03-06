﻿using Abp;
using Abp.Application.Services;
using Abp.Dependency;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Core.Http.Client.DynamicProxying;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Http.Client.DependencyInjection
{
    public static class ServiceCollectionDynamicHttpClientProxyExtensions
    {
        private static readonly ProxyGenerator ProxyGeneratorInstance = new ProxyGenerator();

        /// <summary>
        /// Registers HTTP Client Proxies for all public interfaces
        /// extend the <see cref="IRemoteService"/> interface in the
        /// given <paramref name="assembly"/>.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assembly">The assembly containing the service interfaces</param>
        /// <param name="remoteServiceConfigurationName">
        /// The name of the remote service configuration to be used by the HTTP Client proxies.
        /// See <see cref="AbpRemoteServiceOptions"/>.
        /// </param>
        /// <param name="asDefaultServices">
        /// True, to register the HTTP client proxy as the default implementation for the services.
        /// </param>
        public static IServiceCollection AddHttpClientProxies(
            [NotNull] this IServiceCollection services,
            [NotNull] Assembly assembly,
            [NotNull] string remoteServiceConfigurationName = RemoteServiceConfigurationDictionary.DefaultName,
            bool asDefaultServices = true)
        {
            Check.NotNull(services, nameof(assembly));

            //TODO: Make a configuration option and add remoteServiceName inside it!
            //TODO: Add option to change type filter

            var serviceTypes = assembly.GetTypes().Where(t =>
                t.IsInterface && t.IsPublic && typeof(IApplicationService).IsAssignableFrom(t)
            );

            foreach (var serviceType in serviceTypes)
            {
                services.AddHttpClientProxy(
                    serviceType,
                    remoteServiceConfigurationName,
                    asDefaultServices
                    );
            }

            return services;
        }

        /// <summary>
        /// Registers HTTP Client Proxy for given service type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the service</typeparam>
        /// <param name="services">Service collection</param>
        /// <param name="remoteServiceConfigurationName">
        /// The name of the remote service configuration to be used by the HTTP Client proxy.
        /// See <see cref="AbpRemoteServiceOptions"/>.
        /// </param>
        /// <param name="asDefaultService">
        /// True, to register the HTTP client proxy as the default implementation for the service <typeparamref name="T"/>.
        /// </param>
        public static IServiceCollection AddHttpClientProxy<T>(
            [NotNull] this IServiceCollection services,
            [NotNull] string remoteServiceConfigurationName = RemoteServiceConfigurationDictionary.DefaultName,
            bool asDefaultService = true)
        {
            return services.AddHttpClientProxy(
                typeof(T),
                remoteServiceConfigurationName,
                asDefaultService
            );
        }

        /// <summary>
        /// Registers HTTP Client Proxy for given service <paramref name="type"/>.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="type">Type of the service</param>
        /// <param name="remoteServiceConfigurationName">
        /// The name of the remote service configuration to be used by the HTTP Client proxy.
        /// See <see cref="AbpRemoteServiceOptions"/>.
        /// </param>
        /// <param name="asDefaultService">
        /// True, to register the HTTP client proxy as the default implementation for the service <paramref name="type"/>.
        /// </param>
        public static IServiceCollection AddHttpClientProxy(
            [NotNull] this IServiceCollection services,
            [NotNull] Type type,
            [NotNull] string remoteServiceConfigurationName = RemoteServiceConfigurationDictionary.DefaultName,
            bool asDefaultService = true)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(type, nameof(type));
            Check.NotNullOrWhiteSpace(remoteServiceConfigurationName, nameof(remoteServiceConfigurationName));

            //services.Configure<AbpHttpClientOptions>(options =>
            //{
            //    options.HttpClientProxies[type] = new DynamicHttpClientProxyConfig(type, remoteServiceConfigurationName);
            //});

            var options = IocManager.Instance.Resolve<IAbpHttpClientOptions>();
            options.HttpClientProxies[type] = new DynamicHttpClientProxyConfig(type, remoteServiceConfigurationName);

            //use IHttpClientFactory and polly
            services.AddHttpClient(remoteServiceConfigurationName)
                .AddTransientHttpErrorPolicy(builder =>
                    // retry 3 times
                    builder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i))));

            var interceptorType = typeof(DynamicHttpProxyInterceptor<>).MakeGenericType(type);
            IocManager.Instance.Register(interceptorType, DependencyLifeStyle.Transient);

            // 使用工厂方法注册接口，返回接口代理
            IocManager.Instance.IocContainer.Register(
                Component.For(type)
                    .UsingFactoryMethod(kernel =>
                    {
                        return ProxyGeneratorInstance.CreateResolvableInterfaceProxyWithoutTarget(
                                type,
                                null,
                                (IInterceptor)kernel.Resolve(interceptorType)
                            );
                    }).LifestyleTransient()
                );

            return services;
        }
    }
}
