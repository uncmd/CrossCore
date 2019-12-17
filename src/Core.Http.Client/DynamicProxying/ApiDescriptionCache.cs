using Abp.Dependency;
using Core.Http.Modeling;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Http.Client.DynamicProxying
{
    public class ApiDescriptionCache : IApiDescriptionCache, ISingletonDependency
    {
        // TODO: ICancellationTokenProvider

        private readonly Dictionary<string, ApplicationApiDescriptionModel> _cache;
        private readonly SemaphoreSlim _semaphore;

        public ApiDescriptionCache()
        {
            _cache = new Dictionary<string, ApplicationApiDescriptionModel>();
            _semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task<ApplicationApiDescriptionModel> GetAsync(
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory)
        {
            using (await _semaphore.LockAsync(CancellationToken.None))
            {
                var model = _cache.GetOrDefault(baseUrl);
                if (model == null)
                {
                    _cache[baseUrl] = model = await factory();
                }

                return model;
            }
        }
    }
}
