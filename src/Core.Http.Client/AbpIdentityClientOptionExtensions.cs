using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Core.Http.Client.IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.Client
{
    public static class AbpIdentityClientOptionExtensions
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static AbpIdentityClientOptionExtensions()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            //if (addUserSecrets)
            //{
            //    builder.AddUserSecrets(typeof(AbpIdentityClientOptionExtensions).GetAssembly());
            //}

            return builder.Build();
        }

        public static IAbpIdentityClientOptions HttpClientModule(this IModuleConfigurations configuration)
        {
            return configuration.AbpConfiguration.Get<IAbpIdentityClientOptions>();
        }
    }
}
