using Abp;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Extensions;
using Core.Http.Modeling;
using Core.Http.ProxyScripting.Configuration;
using Core.Http.ProxyScripting.Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http.ProxyScripting
{
    public class ProxyScriptManager : IProxyScriptManager, ITransientDependency
    {
        private readonly IApiDescriptionModelProvider _modelProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProxyScriptManagerCache _cache;
        private readonly AbpApiProxyScriptingOptions _options;

        public ProxyScriptManager(
            IApiDescriptionModelProvider modelProvider,
            IServiceProvider serviceProvider,
            IProxyScriptManagerCache cache,
            IOptions<AbpApiProxyScriptingOptions> options)
        {
            _modelProvider = modelProvider;
            _serviceProvider = serviceProvider;
            _cache = cache;
            _options = options.Value;
        }

        public string GetScript(ProxyScriptingModel scriptingModel)
        {
            var cacheKey = CreateCacheKey(scriptingModel);

            if (scriptingModel.UseCache)
            {
                return _cache.GetOrAdd(cacheKey, () => CreateScript(scriptingModel));
            }

            var script = CreateScript(scriptingModel);
            _cache.Set(cacheKey, script);
            return script;
        }

        private string CreateScript(ProxyScriptingModel scriptingModel)
        {
            var apiModel = _modelProvider.CreateApiModel();

            if (scriptingModel.IsPartialRequest())
            {
                apiModel = apiModel.CreateSubModel(scriptingModel.Modules, scriptingModel.Controllers, scriptingModel.Actions);
            }

            var generatorType = _options.Generators.GetOrDefault(scriptingModel.GeneratorType);
            if (generatorType == null)
            {
                throw new AbpException($"Could not find a proxy script generator with given name: {scriptingModel.GeneratorType}");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService(generatorType).As<IProxyScriptGenerator>().CreateScript(apiModel);
            }
        }

        private string CreateCacheKey(ProxyScriptingModel model)
        {
            return JsonConvert.SerializeObject(model).ToMd5();
        }
    }
}
