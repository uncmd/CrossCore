using Abp;
using Abp.Dependency;
using Abp.Extensions;
using Core.Http.Modeling;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Http.Client.DynamicProxying
{
    public class ApiDescriptionFinder : IApiDescriptionFinder, ITransientDependency
    {
        // TODO: ICancellationTokenProvider

        protected IDynamicProxyHttpClientFactory HttpClientFactory { get; }

        protected IApiDescriptionCache Cache { get; }

        private static readonly JsonSerializerSettings SharedJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ApiDescriptionFinder(
            IApiDescriptionCache cache,
            IDynamicProxyHttpClientFactory httpClientFactory)
        {
            Cache = cache;
            HttpClientFactory = httpClientFactory;
        }

        public async Task<ActionApiDescriptionModel> FindActionAsync(string baseUrl, Type serviceType, MethodInfo method)
        {
            var apiDescription = await GetApiDescriptionAsync(baseUrl);

            //TODO: Cache finding?

            var methodParameters = method.GetParameters().ToArray();

            foreach (var module in apiDescription.Modules.Values)
            {
                foreach (var controller in module.Controllers.Values)
                {
                    if (!controller.Implements(serviceType))
                    {
                        continue;
                    }

                    foreach (var action in controller.Actions.Values)
                    {
                        if (action.Name == method.Name && action.ParametersOnMethod.Count == methodParameters.Length)
                        {
                            var found = true;

                            for (int i = 0; i < methodParameters.Length; i++)
                            {
                                if (action.ParametersOnMethod[i].TypeAsString != methodParameters[i].ParameterType.GetFullNameWithAssemblyName())
                                {
                                    found = false;
                                    break;
                                }
                            }

                            if (found)
                            {
                                return action;
                            }
                        }
                    }
                }
            }

            throw new AbpException($"Could not found remote action for method: {method} on the URL: {baseUrl}");
        }

        public virtual async Task<ApplicationApiDescriptionModel> GetApiDescriptionAsync(string baseUrl)
        {
            return await Cache.GetAsync(baseUrl, () => GetApiDescriptionFromServerAsync(baseUrl));
        }

        protected virtual async Task<ApplicationApiDescriptionModel> GetApiDescriptionFromServerAsync(string baseUrl)
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = await client.GetAsync(
                    baseUrl.EnsureEndsWith('/') + "api/abp/api-definition",
                    CancellationToken.None
                );

                if (!response.IsSuccessStatusCode)
                {
                    throw new AbpException("Remote service returns error! StatusCode = " + response.StatusCode);
                }

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject(
                    content,
                    typeof(ApplicationApiDescriptionModel), SharedJsonSerializerSettings);

                return (ApplicationApiDescriptionModel)result;
            }
        }
    }
}
