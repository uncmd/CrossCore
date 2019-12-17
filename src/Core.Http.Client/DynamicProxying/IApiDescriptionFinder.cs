using Core.Http.Modeling;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Http.Client.DynamicProxying
{
    public interface IApiDescriptionFinder
    {
        Task<ActionApiDescriptionModel> FindActionAsync(string baseUrl, Type serviceType, MethodInfo invocationMethod);

        Task<ApplicationApiDescriptionModel> GetApiDescriptionAsync(string baseUrl);
    }
}
