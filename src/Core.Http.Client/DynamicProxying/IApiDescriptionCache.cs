using Core.Http.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Client.DynamicProxying
{
    public interface IApiDescriptionCache
    {
        Task<ApplicationApiDescriptionModel> GetAsync(
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory
        );
    }
}
